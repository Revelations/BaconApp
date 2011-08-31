//
//  Interpreter.h
//  BaconApp
//
//  Created by Jordan on 31/08/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

// Testing commits with comments.

#import <Foundation/Foundation.h>


@interface Interpreter : NSObject {
    NSString * input;
}

-(NSString *) input;

-(void) setInput: (NSString *) i;

-(Point) getCoordinatesFromInput;

-(NSString *) getFileFromInput;

-(void) isValidFile: (NSString *) s;

@end
