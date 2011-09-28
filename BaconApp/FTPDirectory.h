//
//  FTPDirectory.h
//  BaconApp
//
//  Created by Donovan Hoffman on 25/09/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>
#include <sys/socket.h>
#include <sys/dirent.h>
#include <CFNetwork/CFNetwork.h>


@interface FTPDirectory : NSObject <NSStreamDelegate> {

}

// Properties that don't need to be seen by the outside world.

@property (nonatomic, readonly) BOOL              isReceiving;
@property (nonatomic, retain)   NSInputStream *   networkStream;
@property (nonatomic, retain)   NSMutableData *   listData;
@property (nonatomic, retain)   NSMutableArray *  listEntries;

- (void)startReceive:(NSString *) address;


- (void)_addListEntries:(NSArray *)newEntries;

- (void)_parseListData;

- (void)stream:(NSStream *)aStream handleEvent:(NSStreamEvent)eventCode;

@end