	//
//  Update.m
//  BaconApp
//
//  Created by Donovan Hoffman on 1/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "Update.h"


@implementation Update

-(void)getFile:(NSString *)urlPath{
    
    NSURL *url = [NSURL URLWithString:urlPath];
    NSData *urlData = [NSData dataWithContentsOfURL:url];
      
    //checks to see if the urlData has been downloaded
    if(urlData){
        NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
        NSString *documentsDirectory = [paths objectAtIndex:0];
        
         NSArray *values = [urlPath componentsSeparatedByString:@"/"];
        NSString *backend = [NSString stringWithFormat:@"%@%@", @"/", [values objectAtIndex:[values count] -1]];
        
        
        NSString *filePath = [NSString stringWithFormat:@"%@%@", documentsDirectory, backend];
        [urlData writeToFile:filePath atomically:YES];
    }	
        else{
            NSLog(@"%@ File Was not created", urlPath);
        }
    }


-(void)getDirectory:(NSString *)urlPath{
    
    NSURL *url = [NSURL URLWithString:[NSString stringWithFormat: @"%@%@", urlPath, @"log.txt"]];
    
    //retrieves the data from the url --Donovan
    NSData *urlData = [NSData dataWithContentsOfURL:url];
    
    if(urlData){
        
        //finds the relevant directory to ensure that iOS does not purge it --Donovan
        NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
        NSString *documentsDirectory = [paths objectAtIndex:0];
        
        NSString *filePath = [NSString stringWithFormat:@"%@%@", documentsDirectory, @"//log.txt"];
        [urlData writeToFile:filePath atomically:YES];
        
        NSString *fileContents = [NSString stringWithContentsOfFile:filePath encoding:NSUTF8StringEncoding error:nil];
        
        NSArray *values = [fileContents componentsSeparatedByString:@","];
                
        
        //iterates over the files it needs to download
        for(int i = 0; i < [values count]; i++){
            NSString *s = [values objectAtIndex:i];
            NSString *filePath = [NSString stringWithFormat:@"%@%@%@", documentsDirectory,@"/", s];
            NSFileManager *fileManager = [NSFileManager defaultManager];
            NSString *retrieveUrl = [NSString stringWithFormat:@"%@%@", urlPath,s]; 
            if ([fileManager fileExistsAtPath:filePath]) {
                
                NSDictionary *attrs = [fileManager attributesOfItemAtPath: filePath error: NULL];
                
                int result = [attrs fileSize];                
                int size = [[values objectAtIndex:++i] intValue];
                
                //checks to see if it already has the file
                if(size != result){	
                    [self getFile:retrieveUrl];
                }
            }
            else{
                i++;
                [self getFile:retrieveUrl];
            }
        }
        }
        else{
            NSLog(@"Directory was not created");
        }    
}   

-(void)upLoadFile:(NSString *)urlPath{
    				
}




//#import "PutController.h"

//#import "AppDelegate.h"

#include <CFNetwork/CFNetwork.h>

/*@interface PutController ()

// Properties that don't need to be seen by the outside world.

@property (nonatomic, readonly) BOOL              isSending;
@property (nonatomic, retain)   NSOutputStream *  networkStream;
@property (nonatomic, retain)   NSInputStream *   fileStream;
@property (nonatomic, readonly) uint8_t *         buffer;
@property (nonatomic, assign)   size_t            bufferOffset;
@property (nonatomic, assign)   size_t            bufferLimit;

@end

@implementation PutController*/

#pragma mark * UI Upload methods

// These methods are used by the core transfer code to update the UI.

- (void)_sendDidStart
{
    /*self.statusLabel.text = @"Sending";
    self.cancelButton.enabled = YES;
    [self.activityIndicator startAnimating];
    [[AppDelegate sharedAppDelegate] didStartNetworking];*/
}

- (void)_updateStatus:(NSString *)statusString
{
/*    assert(statusString != nil);
    self.statusLabel.text = statusString;*/
}

- (void)_sendDidStopWithStatus:(NSString *)statusString
{
    /*if (statusString == nil) {
        statusString = @"Put succeeded";
    }
    self.statusLabel.text = statusString;
    self.cancelButton.enabled = NO;
    [self.activityIndicator stopAnimating];
    [[AppDelegate sharedAppDelegate] didStopNetworking];*/
}

#pragma mark * Network Upload methods



// This is the code that actually does the networking.
NSOutputStream *            _networkStream;
NSInputStream *             _fileStream;
enum {
    kSendBufferSize = 32768
};
uint8_t                     _buffer[kSendBufferSize];
size_t                      _bufferOffset;
size_t                      _bufferLimit;


/*@synthesize networkStream = _networkStream;    need to declare properties for these
@synthesize fileStream    = _fileStream;
@synthesize bufferOffset  = _bufferOffset;
@synthesize bufferLimit   = _bufferLimit;*/

// Because buffer is declared as an array, you have to use a custom getter.  
// A synthesised getter doesn't compile.

- (uint8_t *)buffer
{
    return _buffer;
}

- (BOOL)isSending
{
    return (_networkStream != nil);
}

- (void)_startSend:(NSString *)filePath : (NSString *) uploadPath
{
//    BOOL                    success;

        
    NSURL *                 url;
    CFWriteStreamRef        ftpStream;
    
    assert(filePath != nil);
    assert([[NSFileManager defaultManager] fileExistsAtPath:filePath]);
    
    assert(_networkStream == nil);      // don't tap send twice in a row!
    assert(_fileStream == nil);         // ditto
    
    // First get and check the URL.
    url = [NSURL URLWithString:uploadPath];
    
    // If the URL is bogus, let the user know.  Otherwise kick off the connection.
    
    // else {
        
        // Open a stream for the file we're going to send.  We do not open this stream; 
        // NSURLConnection will do it for us.
        
        _fileStream = [NSInputStream inputStreamWithFileAtPath:filePath];
        assert(_fileStream != nil);
        
        [_fileStream open];
        
        // Open a CFFTPStream for the URL.
        
        ftpStream = CFWriteStreamCreateWithFTPURL(NULL, (CFURLRef) url);
        assert(ftpStream != NULL);
        
        _networkStream = (NSOutputStream *) ftpStream;
        
    
    //setting password and username
    
        /*if (self.usernameText.text.length != 0) {
#pragma unused (success) //Adding this to appease the static analyzer.
            success = [self.networkStream setProperty:self.usernameText.text forKey:(id)kCFStreamPropertyFTPUserName];
            assert(success);
            success = [self.networkStream setProperty:self.passwordText.text forKey:(id)kCFStreamPropertyFTPPassword];
            assert(success);
        }*/
        
        _networkStream.delegate = self;
        [_networkStream scheduleInRunLoop:[NSRunLoop currentRunLoop] forMode:NSDefaultRunLoopMode];
        [_networkStream open];
        
        // Have to release ftpStream to balance out the create.
        
        CFRelease(ftpStream);
        
        // Tell the UI we're sending.
        
        [self _sendDidStart];
}


//method called when the stream has stopped for whatever reason, deals with cleanup to make sure nothing is left dangling
- (void)_stopSendWithStatus:(NSString *)statusString
{
    if (_networkStream != nil) {
        [_networkStream removeFromRunLoop:[NSRunLoop currentRunLoop] forMode:NSDefaultRunLoopMode];
       // _networkStream.delegate = nil;
        [_networkStream close];
        //_networkStream = nil;
    }
    if (_fileStream != nil) {
        [_fileStream close];
        //_fileStream = nil;
    }
    [self _sendDidStopWithStatus:statusString];
}

// An NSStream delegate callback that's called when events happen on the 
// network stream.
- (void)stream:(NSStream *)aStream handleEvent:(NSStreamEvent)eventCode
{
//#pragma unused(aStream)
    assert(aStream == _networkStream);
    
    switch (eventCode) {
        case NSStreamEventOpenCompleted: {
            [self _updateStatus:@"Opened connection"];
            } break;
        case NSStreamEventHasBytesAvailable: {
            assert(NO);     // should never happen for the output stream
        } break;
        case NSStreamEventHasSpaceAvailable: {
            [self _updateStatus:@"Sending"];
            
            // If we don't have any data buffered, go read the next chunk of data.
            
            if (_bufferOffset == _bufferLimit) {
                NSInteger   bytesRead;
                
                bytesRead = [_fileStream read:self.buffer maxLength:kSendBufferSize];
                
                if (bytesRead == -1) {
                    [self _stopSendWithStatus:@"File read error"];
                } else if (bytesRead == 0) {
                    [self _stopSendWithStatus:nil];//has reached the end of the stream for the file ie, its finished normally
                } else {
                    _bufferOffset = 0;
                    _bufferLimit  = bytesRead;
                }
            }
            
            // If we're not out of data completely, send the next chunk.
            
            if (_bufferOffset != _bufferLimit) {
                NSInteger   bytesWritten;
                bytesWritten = [_networkStream write:&_buffer[_bufferOffset] maxLength:_bufferLimit - _bufferOffset];
                assert(bytesWritten != 0);
                if (bytesWritten == -1) {
                    [self _stopSendWithStatus:@"Network write error"];
                } else {
                    _bufferOffset += bytesWritten;
                }
            }
        } break;
        case NSStreamEventErrorOccurred: {
            [self _stopSendWithStatus:@"Stream open error"];
        } break;
        case NSStreamEventEndEncountered: {
            // ignore
        } break;
        default: {
            assert(NO);
        } break;
    }
}

#pragma mark * Actions


//method to initiate the upload
- (IBAction)sendAction:(UIView *)sender
{
   assert( [sender isKindOfClass:[UIView class]] );
    
    if ( ! self.isSending ) {
        NSString *  filePath;
        NSString * urlPath;
        
        // User the tag on the UIButton to determine which image to send.
        
        //	filePath = [[AppDelegate sharedAppDelegate] pathForTestImage:sender.tag];
        filePath = @"/Users/donovanhoffman/test.txt";
        urlPath = @"http://revelations.webhop.org:81/";
        assert(filePath != nil);
        
        [self _startSend:filePath :urlPath];
    }
}


//method to stop the upload
- (IBAction)cancelAction:(id)sender
{
/*#pragma unused(sender)
    [self _stopSendWithStatus:@"Cancelled"];*/
}

- (void)textFieldDidEndEditing:(UITextField *)textField
// A delegate method called by the URL text field when the editing is complete. 
// We save the current value of the field in our settings.
{
}

- (BOOL)textFieldShouldReturn:(UITextField *)textField
// A delegate method called by the URL text field when the user taps the Return 
// key.  We just dismiss the keyboard.
{
/*#pragma unused(textField)
    assert( (textField == self.urlText) || (textField == self.usernameText) || (textField == self.passwordText) );
    [textField resignFirstResponder];
    return NO;*/
    return NO;
}

#pragma mark * View controller boilerplate

/*@synthesize urlText           = _urlText;
@synthesize usernameText      = _usernameText;
@synthesize passwordText      = _passwordText;
@synthesize statusLabel       = _statusLabel;
@synthesize activityIndicator = _activityIndicator;
@synthesize cancelButton      = _cancelButton;*/

- (void)viewDidLoad
{
/*    [super viewDidLoad];
    assert(self.urlText != nil);
    assert(self.usernameText != nil);
    assert(self.passwordText != nil);
    assert(self.statusLabel != nil);
    assert(self.activityIndicator != nil);
    assert(self.cancelButton != nil);
    
    self.urlText.text = [[NSUserDefaults standardUserDefaults] stringForKey:@"PutURLText"];
    // The setup of usernameText and passwordText deferred to -viewWillAppear: 
    // because those values are shared by multiple tabs.
    
    self.activityIndicator.hidden = YES;
    self.statusLabel.text = @"Tap a picture to start the put";
    self.cancelButton.enabled = NO;*/
}

- (void)viewWillAppear:(BOOL)animated
{
/*    [super viewWillAppear:animated];
    self.usernameText.text = [[NSUserDefaults standardUserDefaults] stringForKey:@"Username"];
    self.passwordText.text = [[NSUserDefaults standardUserDefaults] stringForKey:@"Password"];*/
}

- (void)viewDidUnload
{
/*    [super viewDidUnload];
    
    self.urlText = nil;
    self.usernameText = nil;
    self.passwordText = nil;
    self.statusLabel = nil;
    self.activityIndicator = nil;
    self.cancelButton = nil;*/
}

- (void)dealloc
{
/*    [self _stopSendWithStatus:@"Stopped"];
    
    [self->_urlText release];
    [self->_usernameText release];
    [self->_passwordText release];
    [self->_statusLabel release];
    [self->_activityIndicator release];
    [self->_cancelButton release];
    */
    [super dealloc];
}

@end

