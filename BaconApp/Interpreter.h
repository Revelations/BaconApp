//
//  Interpreter.h
//  BaconApp
//
//  Created by Jordan on 31/08/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>


@interface Interpreter : NSObject
{
    NSString * storedInputString;
}

@property(nonatomic, retain) NSString * storedInputString;

-(int) x;
-(int) x:(NSString *) inputString;

-(int) y;
-(int) y:(NSString *) inputString;

-(NSString *) htmlPath;
-(NSString *) htmlPath:(NSString *) inputString;

@end
