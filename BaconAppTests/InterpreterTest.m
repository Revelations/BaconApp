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

- (void) setUp
{
    [super setUp];
    
    // Instantiate a new Interpreter for each test.
    interpreter = [Interpreter new];
    
}

- (void) tearDown
{
    // Release the Interpreter from memory.
    [interpreter release];
    
    [super tearDown];
}

-(void) testInput
{
    STfail(@"Not yet implemented.");
}

-(void) testCoordinates
{
    STFail(@"Not yet implemented.");
}

-(void) testFileString
{
    STFail(@"Not yet implemented.");
}
@end
