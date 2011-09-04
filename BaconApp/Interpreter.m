//
//  Interpreter.m
//  BaconApp
//
//  Created by Jordan on 31/08/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "Interpreter.h"


@implementation Interpreter

@synthesize storedInputString;

-(int) x
{
    return [self x:storedInputString];
}

-(int) x:(NSString *)inputString
{
    return [self substringToInt:inputString withRange:NSMakeRange(0, 3)];
}

-(int) y
{
    return [self y:storedInputString];
}

-(int) y:(NSString *)inputString
{
    return [self substringToInt:inputString withRange:NSMakeRange(3, 3)];
}

-(int) substringToInt:(NSString *) inputString withRange:(NSRange) range
{
    NSString * substring = [inputString substringWithRange:range];
    int result = [substring intValue];
    
    return result;
}

-(NSString *) htmlPath
{
    return [self htmlPath:storedInputString];
}

-(NSString *) htmlPath:(NSString *)inputString
{
    return [inputString substringFromIndex:6];
}



@end
