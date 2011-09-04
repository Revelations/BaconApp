//
//  InterpreterTest.m
//  BaconApp
//
//  Created by Jordan on 31/08/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "InterpreterTest.h"
#import "Interpreter.h"


@implementation InterpreterTest

-(void) setUp
{
    interpreter = [Interpreter new];
}

-(void) tearDown
{
    [interpreter release];
}

-(void) testXCoord
{
    int result1 = [interpreter x:@"12345"];
    int result2 = [interpreter x:@"685Hello"];
    int result3 = [interpreter x:@"Houston, we have a problem"];
    
    STAssertEquals(123, result1, @"");
    STAssertEquals(685, result2, @"");
    STAssertEquals(0,   result3, @"");
}

-(void) testYCoord
{
    int result1 = [interpreter y:@"123456"];
    int result2 = [interpreter y:@"685927Hello"];
    int result3 = [interpreter y:@"Houston, we have a problem"];

    STAssertEquals(457,	result1, @"");
    STAssertEquals(927,	result2, @"");
    STAssertEquals(0,	result3, @"");
	STAssertEquals(0,	[interpreter y:@"12345"], @"");
}

-(void) testPath
{
    NSString * result1 = [interpreter htmlPath:@"123456Hello"];
    NSString * result2 = [interpreter htmlPath:@"345678World"];
	NSString * result3 = [interpreter htmlPath:@"424242"];
    
    STAssertEqualObjects(result1, @"Hello", result1);
    STAssertEqualObjects(result2, @"World", result2);
	STAssertEqualObjects(result3, @"", result3);
}

@end
