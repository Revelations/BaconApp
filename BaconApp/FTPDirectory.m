//
//  FTPDirectory.m
//  BaconApp
//
//  Created by Donovan Hoffman on 25/09/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "FTPDirectory.h"
#import <CFNetwork/CFNetwork.h>


@implementation FTPDirectory

@synthesize networkStream   = _networkStream;
@synthesize listData        = _listData;
@synthesize listEntries     = _listEntries;

- (BOOL)isReceiving
{
    return (self.networkStream != nil);
}

- (void)_startReceive:(NSString *) address
// Starts a connection to download the current URL.
{
    BOOL                success;
    NSURL *             url;
    CFReadStreamRef     ftpStream;
    
    assert(self.networkStream == nil);      // don't tap receive twice in a row!
    url =  [NSURL URLWithString:address];
    self.listData = [NSMutableData data];
    assert(self.listData != nil);
        
    // Open a CFFTPStream for the URL.
    ftpStream = CFReadStreamCreateWithFTPURL(NULL, (CFURLRef) url);
    assert(ftpStream != NULL);
    self.networkStream = (NSInputStream *) ftpStream;
    
    //gets the stream going
    self.networkStream.delegate = self;
    [self.networkStream scheduleInRunLoop:[NSRunLoop currentRunLoop] forMode:NSDefaultRunLoopMode];
    [self.networkStream open];
    
    //tidyup
    CFRelease(ftpStream);
    [self _receiveDidStart];    
}

- (void)_addListEntries:(NSArray *)newEntries
{
    assert(self.listEntries != nil);
    
    [self.listEntries addObjectsFromArray:newEntries];
}

- (void)_parseListData
{
    NSMutableArray *    newEntries;
    NSUInteger          offset;
    
    // accumulate the new entries into an array to avoid a) adding items to the 
    // table one-by-one, and b) repeatedly shuffling the listData buffer around.
    
    newEntries = [NSMutableArray array];
    assert(newEntries != nil);
    
    offset = 0;
    do {
        CFIndex         bytesConsumed;
        CFDictionaryRef thisEntry;
        
        thisEntry = NULL;
        
        assert(offset <= self.listData.length);
        bytesConsumed = CFFTPCreateParsedResourceListing(NULL, &((const uint8_t *) self.listData.bytes)[offset], self.listData.length - offset, &thisEntry);
        if (bytesConsumed > 0) {
            
            // It is possible for CFFTPCreateParsedResourceListing to return a 
            // positive number but not create a parse dictionary.  For example, 
            // if the end of the listing text contains stuff that can't be parsed, 
            // CFFTPCreateParsedResourceListing returns a positive number (to tell 
            // the caller that it has consumed the data), but doesn't create a parse 
            // dictionary (because it couldn't make sense of the data).  So, it's 
            // important that we check for NULL.
            
            if (thisEntry != NULL) {
                NSDictionary *  entryToAdd;
                
                // Try to interpret the name as UTF-8, which makes things work properly 
                // with many UNIX-like systems, including the Mac OS X built-in FTP 
                // server.  If you have some idea what type of text your target system 
                // is going to return, you could tweak this encoding.  For example, 
                // if you know that the target system is running Windows, then 
                // NSWindowsCP1252StringEncoding would be a good choice here.
                // 
                // Alternatively you could let the user choose the encoding up 
                // front, or reencode the listing after they've seen it and decided 
                // it's wrong.
                //
                // Ain't FTP a wonderful protocol!
                
                entryToAdd = [self _entryByReencodingNameInEntry:(NSDictionary *) thisEntry encoding:NSUTF8StringEncoding];
                
                [newEntries addObject:entryToAdd];
            }
            
            // We consume the bytes regardless of whether we get an entry.
            
            offset += bytesConsumed;
        }
        
        if (thisEntry != NULL) {
            CFRelease(thisEntry);
        }
        
        if (bytesConsumed == 0) {
            // We haven't yet got enough data to parse an entry.  Wait for more data 
            // to arrive.
            break;
        } else if (bytesConsumed < 0) {
            // We totally failed to parse the listing.  Fail.
            [self _stopReceiveWithStatus:@"Listing parse failed"];
            break;
        }
    } while (YES);
    
    if (newEntries.count != 0) {
        [self _addListEntries:newEntries];
    }
    if (offset != 0) {
        [self.listData replaceBytesInRange:NSMakeRange(0, offset) withBytes:NULL length:0];
    }
}

- (void)stream:(NSStream *)aStream handleEvent:(NSStreamEvent)eventCode
// An NSStream delegate callback that's called when events happen on our 
// network stream.
{
#pragma unused(aStream)
    assert(aStream == self.networkStream);
    
    switch (eventCode) {
        case NSStreamEventOpenCompleted: {
            NSLog(@"Opened connection");
        } break;
        case NSStreamEventHasBytesAvailable: {
            NSInteger       bytesRead;
            uint8_t         buffer[32768];
            
            NSLog(@"Receiving");
//            [self _updateStatus:@"Receiving"];
            
            // Pull some data off the network.
            
            
            bytesRead = [self.networkStream read:buffer maxLength:sizeof(buffer)];
            if (bytesRead == -1) {
                NSLog(@"Network read error");
                return;
            } else if (bytesRead == 0) {
                NSLog(@"stopReceiveWithStatus:nil");
            } else {
                assert(self.listData != nil);
                
                // Append the data to our listing buffer.
                
                [self.listData appendBytes:buffer length:bytesRead];
                
                // Check the listing buffer for any complete entries and update 
                // the UI if we find any.
                
                [self _parseListData];
            }
        } break;
        case NSStreamEventHasSpaceAvailable: {
            assert(NO);     // should never happen for the output stream
        } break;
        case NSStreamEventErrorOccurred: {
            NSLog(@"Stream open error");
        } break;
        case NSStreamEventEndEncountered: {
            // ignore
        } break;
        default: {
            assert(NO);
        } break;
    }
}
- (void)dealloc
{
    NSLog(@"Stopped");
    
    [self->_listEntries release];
    
    [super dealloc];
}
@end
