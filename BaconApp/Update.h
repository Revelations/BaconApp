//
//  Update.h
//  BaconApp
//
//  Created by Donovan Hoffman on 1/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "Reachability.h"

@interface Update : NSObject <NSStreamDelegate> {
	
}

@property (nonatomic, readonly) BOOL              isSendingU;
@property (nonatomic, retain)   NSOutputStream *  networkStreamU;
@property (nonatomic, retain)   NSInputStream *   fileStreamU;
@property (nonatomic, readonly) uint8_t *         bufferU;
@property (nonatomic, assign)   size_t            bufferOffsetU;
@property (nonatomic, assign)   size_t            bufferLimitU;

-(void)getFile:(NSString *)urlPath:(NSString *)filePath;
-(void)getDirectory:(NSString *)urlPath;
-(int)CheckForInternet: (Reachability *) curReach;
//-(void)upLoadFile:(NSString *)urlPath;
//-(void)_startSend:(NSString *)filePath : (NSString *) urlPath;
-(void)uploadPhp:(NSString *) filePath;
-(void)GetGameFiles:(NSString *) urlPath;

@end
