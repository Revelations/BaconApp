//
//  Quiz.m
//  BaconApp
//
//  Created by Donovan Hoffman on 14/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "Quiz.h"

@implementation Quiz

@synthesize questions = _questions;
@synthesize questionName = _questionName;

 int const question = 0;
 int const option1 = 1;
 int const option2 = 2;
 int const option3 = 3;
 int const option4 = 4;
 int const answer = 5;

#pragma mark - Methods
-(void)readQuizFile: (NSString *) filePath {
	
	NSString * newline = @"\r\n";
	NSString * fileContent = [[[NSString alloc] initWithContentsOfFile:filePath] autorelease];
	NSArray * contents = [fileContent componentsSeparatedByString:newline];
	NSMutableArray * question = [[NSMutableArray alloc] init];
	
	for (int i =0; i < [contents count]; i++) {
		for (int k = 0; k < 6; k++) {
			NSString * s = [contents objectAtIndex:i];
			[[self questions] addObject:s];
			[question addObject:s];
		}
	}
}
@end
