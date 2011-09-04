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
    
    STAssertEquals(result1, 123, @"");
    STAssertEquals(result2, 685, @"");
    STAssertEquals(result3, 0, @"");
}

-(void) testYCoord
{
    int result1 = [interpreter y:@"123456"];
    int result2 = [interpreter y:@"685927Hello"];
    int result3 = [interpreter y:@"Houston, we have a problem"];

    STAssertEquals(result1, 456, @"");
    STAssertEquals(result2, 927, @"");
    STAssertEquals(result3, 0, @"");
}

-(void) testPath
{
    NSString * result1 = [interpreter htmlPath:@"123456Hello"];
    NSString * result2 = [interpreter htmlPath:@"345678World"];
    
    STAssertEqualObjects(result1, @"Hello", result1);
    STAssertEqualObjects(result2, @"World", result2);
}

@end
