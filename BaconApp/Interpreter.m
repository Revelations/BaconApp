//
//  Interpreter.m
//  BaconApp
//
//  Created by Jordan on 31/08/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "Interpreter.h"


@implementation Interpreter

-(int) xCoord:(NSString *)inputString
{
    return [self substringToInt:inputString withRange:NSMakeRange(0, 3)];
}

-(int) yCoord:(NSString *)inputString
{
    return [self substringToInt:inputString withRange:NSMakeRange(3, 3)];
}

-(int) substringToInt:(NSString *) inputString withRange:(NSRange) range
{
    NSString * substring = [inputString substringWithRange:range];
    int result = [substring intValue];
    
    return result;
}

-(NSString *) htmlPath:(NSString *)inputString
{
    return [inputString substringFromIndex:6];
}



@end
