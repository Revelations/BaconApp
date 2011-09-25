//
//  FTPFile.h
//  BaconApp
//
//  Created by Donovan Hoffman on 25/09/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>
#include <sys/socket.h>
#include <sys/dirent.h>
#include <CFNetwork/CFNetwork.h>

@interface FTPFile : NSObject {
    
}
@property (nonatomic, retain) NSOutputStream *  networkStreamOutput;
@property (nonatomic, retain) NSInputStream *   networkStreamInput;
@property (nonatomic,retain)  NSOutputStream *  fileStream;
@end
