//
//  Scanner.h
//  BaconApp
//
//  Created by Jordan on 31/08/11.
//  Copyright 2011 Team Bacon. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface Scanner : NSObject <ZBarReaderDelegate>
{
	NSString * outputString;
}

@property(nonatomic, retain) NSString * ouputString;

-(void) scan:(UIViewController *) view;

@end
