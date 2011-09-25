//
//  FTPFile.m
//  BaconApp
//
//  Created by Donovan Hoffman on 25/09/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "FTPFile.h"


@implementation FTPFile

@synthesize networkStreamOutput;
@synthesize networkStreamInput;
@synthesize fileStream;

-(void)DownloadFile:(NSString *)address:(NSString *) filePath{ 
    
    NSURL *             url;
    CFReadStreamRef     ftpStream;
    
    
    url =  [NSURL URLWithString:address];
    
    NSLog(@"Connecting..");
    self.fileStream = [NSOutputStream outputStreamToFileAtPath:filePath append:NO];
    [self.fileStream open];
    
    //open a CFFTPStream for the URL
    
    ftpStream = CFReadStreamCreateWithFTPURL(NULL, (CFURLRef)url);
    assert(ftpStream != NULL);
    self.networkStreamInput = (NSInputStream*) ftpStream;
    self.networkStreamInput.delegate = self;
    [self.networkStreamInput scheduleInRunLoop:[NSRunLoop currentRunLoop] forMode:NSDefaultRunLoopMode];
    [self.networkStreamInput open];
    CFRelease(ftpStream);
}

- (void)stream:(NSStream *)aStream handleEventDL:(NSStreamEvent)eventCode
// An NSStream delegate callback that's called when events happen on our 
// network stream.
{
#pragma unused(aStream)
    assert(aStream == self.networkStreamInput);
    
    switch (eventCode) {
        case NSStreamEventOpenCompleted: {
            NSLog(@"Opened connection");
        } break;
        case NSStreamEventHasBytesAvailable: {
            NSInteger       bytesRead;
            uint8_t         buffer[32768];
            
            // Pull some data off the network.
            
            bytesRead = [self.networkStreamInput read:buffer maxLength:sizeof(buffer)];
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
                    bytesWritten = [fileStream write:&buffer[bytesWrittenSoFar] maxLength:bytesRead - bytesWrittenSoFar];
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
