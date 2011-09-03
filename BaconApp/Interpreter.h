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

-(int) xCoord;
-(int) xCoord:(NSString *) inputString;

-(int) yCoord;
-(int) yCoord:(NSString *) inputString;

-(int) substringToInt:(NSString *) inputString withRange:(NSRange) range;

-(NSString *) htmlPath;
-(NSString *) htmlPath:(NSString *) inputString;

@end
