//
//  Interpreter.m
//  BaconApp
//
//  Created by Jordan on 31/08/11.
//  Copyright 2011 Team Bacon. All rights reserved.
//

#import "Interpreter.h"


@implementation Interpreter

@synthesize storedInputString;


-(int) substringToInt:(NSString *)inputString withRange:(NSRange)range
{
    NSString * substring = [inputString substringWithRange:range];
    int result = [substring intValue];
    
    return result;
}

// Gets the last known x coordinate.
-(int) x
{
	return _x;
}

// Extract the x coordinate from the input string.
-(int) x:(NSString *)inputString
{
    return [self substringToInt:inputString withRange:NSMakeRange(0, 3)];
}

// Gets the last known y-coordinate
-(int) y
{
	return _y;
}

// Extract the y coordinate from the input string.
-(int) y:(NSString *)inputString
{
	_y = [self substringToInt:inputString withRange:NSMakeRange(3, 3)];
    return _y;
}

-(NSString *) htmlPath
{
	return _html;
//    return [self htmlPath:storedInputString];
}

-(NSString *) htmlPath:(NSString *)inputString
{
	_html = [inputString substringFromIndex:6];
    return _html;
}

@end
