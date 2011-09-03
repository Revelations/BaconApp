//
//  BaconTest.m
//  ViewApp
//
//  Created by Shii Kayano on 3/09/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

/*
 
 Should we set the center of the screen as (0,0)? In which case, we'll need offset values. I'll leave that to you, Donny.

 Also, I propose a "you are here, off the screen" idea. :D
 Think "Worms", or any game that allows stuff to go off screen but not follow it.
 
 */


#import "BaconTest.h"


@implementation BaconTest

#if USE_APPLICATION_UNIT_TEST     // all code under test is in the iPhone Application

- (void) testAppDelegate {
    id yourApplicationDelegate = [[UIApplication sharedApplication] delegate];
    STAssertNotNil(yourApplicationDelegate, @"UIApplication failed to find the AppDelegate");
}

#else                           // all code under test must be linked into the Unit Test bundle

- (void) testMath {
    STAssertTrue((1+1)==2, @"Compiler isn't feeling well today :-(" );
}

#endif

-(void)setUp {
	bacon = [[Bacon alloc] initWithX:42 andY:108 andFile:@"helloworld"];
	STAssertNotNil(bacon, @"Object did not initialise successfully.");
}
-(void)tearDown {
	[bacon release];
}

-(void)testGetMapX {
	int expected = 42;
	int result = bacon.map_x;
	STAssertEquals(expected, result,
				   @"We expected %d, but it was %d",expected,result);
}

-(void)testGetMapY {
	int expected = 108;
	int result = bacon.map_y;
	STAssertEquals(expected, result,
				   @"We expected %d, but it was %d",expected,result);	
}

-(void)testGetMapFile {
	NSString *expected = @"/files/info/helloworld.html";
	NSString *result = bacon.map_file;
	STAssertEqualObjects(expected, result,
						 @"We expected %@, but it was %@",expected,result);	
}

-(void)testGetMapUrl {
	NSString *expected = @"/files/info/helloworld.html?map_x=42&map_y=108";
	NSString *result = bacon.map_url;
	STAssertEqualObjects(expected, result,
						 @"We expected %@, but it was %@",expected,result);
}

@end
