//
//  UpdateController.m
//  BaconApp
//
//  Created by Donovan Hoffman on 25/09/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "UpdateController.h"
#import <CFNetwork/CFNetwork.h>

@implementation UpdateController

NSOutputStream * _networkStreamO;
NSInputStream * _networkStreamI;
NSOutputStream *  fileStream;

- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        // Custom initialization
    }
    return self;
}

- (void)dealloc
{
    [_networkStreamI release];
    [_networkStreamO release];
    [super dealloc];
}

- (void)didReceiveMemoryWarning
{
    // Releases the view if it doesn't have a superview.
    [super didReceiveMemoryWarning];
    
    // Release any cached data, images, etc that aren't in use.
}

-(IBAction) Update{
    
}

-(IBAction) NoUpdate{
    
}


- (void)viewDidLoad
{
    [super viewDidLoad];
    // Do any additional setup after loading the view from its nib.
}

- (void)viewDidUnload
{
    [super viewDidUnload];
    // Release any retained subviews of the main view.
    // e.g. self.myOutlet = nil;
}

- (BOOL)shouldAutorotateToInterfaceOrientation:(UIInterfaceOrientation)interfaceOrientation
{
    // Return YES for supported orientations
    return (interfaceOrientation == UIInterfaceOrientationPortrait);
}






-(void)DownloadDirectoryListing: (NSString *) address{
    //@synthesize networkStream = _networkStream;//NSOutputStream
    BOOL                success;
    NSURL *             url;
    CFWriteStreamRef    ftpStream;
    
    url =  [NSURL URLWithString:address];
    ftpStream = CFWriteStreamCreateWithFTPURL(NULL, (CFURLRef) url);
    
    assert(ftpStream != NULL);
    
    _networkStreamO = (NSOutputStream *) ftpStream;
   
   #pragma unused (success) //Adding this to appease the static analyzer.
    
    success = [_networkStreamO setProperty:@"username" forKey:(id)kCFStreamPropertyFTPUserName];
    assert(success);
    success = [_networkStreamO setProperty:@"password" forKey:(id)kCFStreamPropertyFTPPassword];
    assert(success);
    //networkStream.delegate = self;
    [_networkStreamO scheduleInRunLoop:[NSRunLoop currentRunLoop] forMode:NSDefaultRunLoopMode];
    [_networkStreamO open];
    CFRelease(ftpStream);
}
- (BOOL)isCreating
{
    return (_networkStreamO != nil);
}

- (void)stream:(NSStream *)aStream handleEvent:(NSStreamEvent)eventCode
// An NSStream delegate callback that's called when events happen on our 
// network stream.
{
#pragma unused(aStream)
    assert(aStream == _networkStreamO);
    
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
           
            err = CFWriteStreamGetError( (CFWriteStreamRef) _networkStreamO);
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
    return (_networkStreamI != nil);
}

-(void)DownloadFile:(NSString *)address:(NSString *) filePath{ 
    
    NSURL *             url;
    CFReadStreamRef     ftpStream;
    
    
    url =  [NSURL URLWithString:address];
 
    NSLog(@"Connecting..");
    fileStream = [NSOutputStream outputStreamToFileAtPath:filePath append:NO];
    [fileStream open];
    
    //open a CFFTPStream for the URL
    
    ftpStream = CFReadStreamCreateWithFTPURL(NULL, (CFURLRef)url);
    assert(ftpStream != NULL);
    _networkStreamI = (NSInputStream*) ftpStream;
    //_networkStreamI.delegate = self;
    [_networkStreamI scheduleInRunLoop:[NSRunLoop currentRunLoop] forMode:NSDefaultRunLoopMode];
    [_networkStreamI open];
    CFRelease(ftpStream);
}

- (void)stream:(NSStream *)aStream handleEventDL:(NSStreamEvent)eventCode
// An NSStream delegate callback that's called when events happen on our 
// network stream.
{
#pragma unused(aStream)
    assert(aStream == _networkStreamI);
    
    switch (eventCode) {
        case NSStreamEventOpenCompleted: {
            NSLog(@"Opened connection");
        } break;
        case NSStreamEventHasBytesAvailable: {
            NSInteger       bytesRead;
            uint8_t         buffer[32768];
            
            // Pull some data off the network.
            
            bytesRead = [_networkStreamI read:buffer maxLength:sizeof(buffer)];
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
