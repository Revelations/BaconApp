//
//  DataItemTest.m
//  BaconApp
//
//  Created by Jordan on 2/09/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "HistoryItemTest.h"


@implementation HistoryItemTest

-(void) setUp
{
    historyItem = [HistoryItem new];
}

-(void) tearDown
{
    [historyItem dealloc];
}

-(void) testHtmlFile
{
    historyItem.htmlFile = @"Menu";
    STAssertEqualObjects(historyItem.htmlFile, @"Menu", nil);
}

-(void) testXCoord
{
    historyItem.x = 5;
    STAssertEquals(historyItem.x, 5, nil);
}

-(void) testYCoord
{
    historyItem.y = 9;
    STAssertEquals(historyItem.y, 9, nil);
}

-(void) testCoordinates
{
    historyItem.x = 5;
    historyItem.y = 9;
    
    STAssertEquals(historyItem.coordinates, CGPointMake(5, 9), nil);
    
    historyItem.y = 15;
    
    STAssertEquals(historyItem.coordinates, CGPointMake(5, 15), nil);
    
    historyItem.coordinates = CGPointMake(14, 7);
    
    STAssertEquals(historyItem.x, 14, nil);
    STAssertEquals(historyItem.y, 7, nil);
}

-(void) testInit
{
    historyItem = [[HistoryItem alloc] initHtmlFile:@"Menu" x:10 y:14];
    
    STAssertEqualObjects(historyItem.htmlFile, @"Menu", nil);
    STAssertEquals(historyItem.coordinates, CGPointMake(10, 14), nil);
    STAssertEquals(historyItem.x, 10, nil);
    STAssertEquals(historyItem.y, 14, nil);
}

@end
