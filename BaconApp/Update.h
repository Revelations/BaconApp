//
//  Update.h
//  BaconApp
//
//  Created by Donovan Hoffman on 1/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>


@interface Update : NSObject <NSStreamDelegate> {
    
}

- (void)getFile:(NSString *) urlPath;
-(void)getDirectory:(NSString *)urlPath;
-(void)upLoadFile:(NSString *)urlPath;
-(void)_startSend:(NSString *)filePath : (NSString *) urlPath;

@end
