//
//  Interpreter.m
//  BaconApp
//
//  Created by Jordan on 31/08/11.
//  Copyright 2011 Team Bacon. All rights reserved.
//

#import "Interpreter.h"
#include "stdio.h"
#include "BaconAppDelegate.h"
#include "Update.h"

@implementation Interpreter

@synthesize storedInputString;
@synthesize map_x,map_y,page_title;

-(NSString *) readLineAsNSString:(FILE *) file
{
	char buffer[4096];
	
	// tune this capacity to your liking -- larger buffer sizes will be faster, but
	// use more memory
	NSMutableString *result = [NSMutableString stringWithCapacity:256];
	
	// Read up to 4095 non-newline characters, then read and discard the newline
	int charsRead;
	do
	{
		if(fscanf(file, "%4095[^\n]%n%*c", buffer, &charsRead) == 1)
			[result appendFormat:@"%s", buffer];
		else
			break;
	} while(charsRead == 4095);
	
	return result;
}

//#pragma mark - Remote Methods
//+(NSString *)	remoteContentDir:(NSString *)urlPath;
//+(NSURL *)		remoteContentLogURL:(NSString *)urlPath;
//+(NSString *)	remoteGameDir:(NSString *)urlPath;
//+(NSURL *)		remoteGameLogURL:(NSString *)urlPath;
//
//#pragma mark - Local Methods
//+(NSString *)	localContentDir;
//+(NSString *)	localContentLogFile;
//+(NSString *)	localGameDir;
//+(NSString *)	localGameLogFile;


-(void) setVals:(NSString *) filePath {
	NSLog(@"arg = %@", filePath);
	NSString * localContentDir = [Update localContentDir];
	const char * fileName = [[NSString stringWithFormat:@"%@/%@.html", localContentDir, filePath] UTF8String];
	FILE * file = fopen(fileName, "r");
	NSLog(@"file: %@, path: %@", file, localContentDir);
	// check for NULL
	while(!feof(file))
	{
		NSString *line = [self readLineAsNSString:file];
		NSArray * contents = [line componentsSeparatedByString:@"<!--"];
		if ([contents count] > 1)
		{
			NSArray * x_contents = [line componentsSeparatedByString:@"x="];
			NSArray * y_contents = [line componentsSeparatedByString:@"y="];
			
			map_x = [[[x_contents objectAtIndex:1] substringWithRange:NSMakeRange(0, 3)] intValue];
			map_y = [[[y_contents objectAtIndex:1] substringWithRange:NSMakeRange(0, 3)] intValue];
		}
		
		NSArray * titles = [line componentsSeparatedByString:@"title>"];
		if ([titles count] > 0)
		{
			NSArray * title_contents = [line componentsSeparatedByString:@"</"] ;
			
			page_title = [title_contents objectAtIndex:0];
		}
		
		// do stuff with line; line is autoreleased, so you should NOT release it (unless you also retain it beforehand)
	}
	fclose(file);
	storedInputString = filePath;
}

-(int) substringToInt:(NSString *)inputString withRange:(NSRange)range
{
	NSString * substring = [inputString substringWithRange:range];
	int result = [substring intValue];
	
	return result;
}

// Gets the last known x coordinate.
-(int) x
{
	//return _x;
	return [self map_x];
}

// Extract the x coordinate from the input string.
/*-(int) x:(NSString *)inputString
 {
 return [self substringToInt:inputString withRange:NSMakeRange(0, 3)];
 }*/

// Gets the last known y-coordinate
-(int) y
{
	//return _y;
	return [self map_y];
}

// Extract the y coordinate from the input string.
/*-(int) y:(NSString *)inputString
 {
 return [self substringToInt:inputString withRange:NSMakeRange(3, 3)];
 //    return _y;
 }*/

-(NSString *) htmlPath
{
	//	return _html;
	return [self storedInputString];
}

/*-(NSString *) htmlPath:(NSString *)inputString
 {
 return [inputString substringFromIndex:6];
 //return _html;
 }*/

@end
