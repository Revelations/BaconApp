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

-(int) x;
-(int) x:(NSString *) inputString;

-(int) y;
-(int) y:(NSString *) inputString;

-(NSString *) htmlPath;
-(NSString *) htmlPath:(NSString *) inputString;

@end
