//
//  FTPUpdater.m
//  BaconApp
//
//  Created by Donoan Hoffman on 25/09/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "FTPUpdater.h"


@implementation FTPUpdater

@synthesize networkStreamOutput;
@synthesize networkStreamInput;
@synthesize	fileWriter;


+(void)DownloadDirectoryListing: (NSString *) address{
    //@synthesize networkStream = _networkStream;//NSOutputStream
    BOOL                success;
    NSURL *             url;
    CFWriteStreamRef    ftpStream;
    
    url =  [NSURL URLWithString:address];
    ftpStream = CFWriteStreamCreateWithFTPURL(NULL, (CFURLRef) url);
    
    assert(ftpStream != NULL);
	networkStreamOutput = (NSOutputStream *) ftpStream;
	
#pragma unused (success) //Adding this to appease the static analyzer.
    
    success = [networkStreamOutput setProperty:@"username" forKey:(id)kCFStreamPropertyFTPUserName];
    assert(success);
    success = [networkStreamOutput setProperty:@"password" forKey:(id)kCFStreamPropertyFTPPassword];
    assert(success);
    networkStreamOutput.delegate = self;
    [networkStreamOutput scheduleInRunLoop:[NSRunLoop currentRunLoop] forMode:NSDefaultRunLoopMode];
    [networkStreamOutput open];
    CFRelease(ftpStream);
}
- (BOOL)isCreating
{
    return (networkStreamOutput != nil);
}

- (void)stream:(NSStream *)aStream handleEvent:(NSStreamEvent)eventCode
// An NSStream delegate callback that's called when events happen on our 
// network stream.
{
#pragma unused(aStream)
    assert(aStream == networkStreamOutput);
    
    switch (eventCode) {
        case NSStreamEventOpenCompleted: {
            NSLog(@"Opened connection");
            // Despite what it says in the documentation <rdar://problem/7163693>, 
            // you should wait for the NSStreamEventEndEncountered event to see 
            // if the directory was created successfully.  If stream shut 
            // down now, errors will be missed coming back from the server in response 
            // to the MKD command.
		} break;
        case NSStreamEventHasBytesAvailable: {
            assert(NO);     // should never happen for the output stream
        } break;
        case NSStreamEventHasSpaceAvailable: {
            assert(NO);
        } break;
        case NSStreamEventErrorOccurred: {
            CFStreamError   err;
            
            // -streamError does not return a useful error domain value, so we 
            // get the old school CFStreamError and check it.
			
            err = CFWriteStreamGetError( (CFWriteStreamRef) networkStreamOutput);
			NSLog([NSString stringWithFormat:@"Ftp error %d", (int) err.error ]);
        } break;
        case NSStreamEventEndEncountered: {
            NSLog(@"stopCreateWithStatus nil: streamEventEndEncountered");
            //[self _stopCreateWithStatus:nil];
        } break;
        default: {
            assert(NO);
        } break;
    }
}

-(BOOL)isReceiving{
    return (networkStreamInput != nil);
}

+(void)DownloadFile:(NSString *)address:(NSString *) filePath{ 
    
    NSURL *             url;
    CFReadStreamRef     ftpStream;
    
    
    url =  [NSURL URLWithString:address];
	
    NSLog(@"Connecting..");
    fileWriter = [NSOutputStream outputStreamToFileAtPath:filePath append:NO];
    [fileWriter open];
    
    //open a CFFTPStream for the URL
    
    ftpStream = CFReadStreamCreateWithFTPURL(NULL, (CFURLRef)url);
    assert(ftpStream != NULL);
    networkStreamInput = (NSInputStream*) ftpStream;
    //_networkStreamI.delegate = self;
    [networkStreamInput scheduleInRunLoop:[NSRunLoop currentRunLoop] forMode:NSDefaultRunLoopMode];
    [networkStreamInput open];
    CFRelease(ftpStream);
}

- (void)stream:(NSStream *)aStream handleEventDL:(NSStreamEvent)eventCode
// An NSStream delegate callback that's called when events happen on our 
// network stream.
{
#pragma unused(aStream)
    assert(aStream == networkStreamInput);
    
    switch (eventCode) {
        case NSStreamEventOpenCompleted: {
            NSLog(@"Opened connection");
        } break;
        case NSStreamEventHasBytesAvailable: {
            NSInteger       bytesRead;
            uint8_t         buffer[32768];
            
            // Pull some data off the network.
            
            bytesRead = [networkStreamInput read:buffer maxLength:sizeof(buffer)];
            if (bytesRead == -1) {
                NSLog(@"Network read error");
            } else if (bytesRead == 0) {
                NSLog(@"stopReceiveWithstatus:nil");
            } else {
                NSInteger   bytesWritten;
                NSInteger   bytesWrittenSoFar;
                
                // Write to the file.
                
                bytesWrittenSoFar = 0;
                do {
                    bytesWritten = [fileWriter write:&buffer[bytesWrittenSoFar] maxLength:bytesRead - bytesWrittenSoFar];
                    assert(bytesWritten != 0);
                    if (bytesWritten == -1) {
                        NSLog(@"File write error:loc1");//loc1
                        break;
                    } else {
                        bytesWrittenSoFar += bytesWritten;
                    }
                } while (bytesWrittenSoFar != bytesRead);
            }
        } break;
        case NSStreamEventHasSpaceAvailable: {
            assert(NO);     // should never happen for the output stream
        } break;
        case NSStreamEventErrorOccurred: {
            NSLog(@"Stream open error: loc1");
        } break;
        case NSStreamEventEndEncountered: {
            // ignore
        } break;
        default: {
            assert(NO);
        } break;
    }
}

@end
