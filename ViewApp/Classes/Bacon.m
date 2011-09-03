//
//  Bacon.m
//  ViewApp
//
//  Created by Shii Kayano on 3/09/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "Bacon.h"


@implementation Bacon
@synthesize map_x, map_y, map_file;

-(id)init {
	return [self initWithX:0 andY:0];
}
-(id)initWithX:(int)x andY:(int)y {
	return [self initWithX:x andY:y andFile:@"default"];
}
-(id)initWithX:(int)x andY:(int)y andFile:(NSString *)file {
	self.map_x = x;
	self.map_y = y;
	self.map_file = file;
	return self;
}

-(void)setMap_file:(NSString *)input {
	NSString *file = [NSString stringWithFormat:@"%@.%@", input, EXT]; //Formats the filename as such: input.ext
	NSArray *array = [NSArray arrayWithObjects: @"", ROOT, INFO, file, nil];
	map_file = [[array valueForKey:@"description"] componentsJoinedByString:DELIM];
}

-(NSString *)map_url {
	return [NSString stringWithFormat:@"%@?map_x=%d&map_y=%d", map_file, map_x, map_y];
}

@end
