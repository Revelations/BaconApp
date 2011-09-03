//
//  Bacon.h
//  ViewApp
//
//  Created by Shii Kayano on 3/09/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>
static NSString *const ROOT		= @"files";
static NSString *const INFO		= @"info";
static NSString *const EXT		= @"html"; //Extension of the webpages.
static NSString *const DELIM	= @"/";

@interface Bacon : NSObject {
	int map_x, map_y;
	NSString *map_file;
}

@property (readwrite) int map_x;
@property (readwrite) int map_y;
@property (readwrite, retain) NSString *map_file;
@property (readonly) NSString *map_url;

-(id)init;
-(id)initWithX:(int)x andY:(int)y;
-(id)initWithX:(int)x andY:(int)y andFile:(NSString *)file;

@end
