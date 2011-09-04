//
//  HistoryItem.m
//  BaconApp
//
//  Created by Jordan on 3/09/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "HistoryItem.h"


@implementation HistoryItem

@synthesize htmlFile, x, y;

-(CGPoint) coordinates
{
    return CGPointMake(x, y);
}

-(void) setCoordinates:(CGPoint)coordinates
{
    x = coordinates.x;
    y = coordinates.y;
}

-(HistoryItem *) initWithHtmlFile:(NSString *)HtmlFile x:(int)xCoord y:(int)yCoord
{
    self = [super init];
    
    if(self)
    {
        self.htmlFile = HtmlFile;
        self.x = xCoord;
        self.y = yCoord;
    }
    
    return self;
}

@end
