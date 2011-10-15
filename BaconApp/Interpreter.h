//
//  Interpreter.h
//  BaconApp
//
//  Created by Jordan on 31/08/11.
//  Copyright 2011 Team Bacon. All rights reserved.
//

#import <Foundation/Foundation.h>


@interface Interpreter : NSObject
{
	NSString * storedInputString;
	//int _x, _y;
	//NSString *_html;
}

@property(nonatomic, retain) NSString * storedInputString;
@property(readwrite) int map_x;
@property(readwrite) int map_y;
@property(nonatomic, retain) NSString * page_title;

-(int) x;
//-(int) x:(NSString *) inputString;

-(int) y;
//-(int) y:(NSString *) inputString;

-(NSString *) htmlPath;
//-(NSString *) htmlPath:(NSString *) inputString;
-(void) setVals: (NSString *) filePath;

@end
