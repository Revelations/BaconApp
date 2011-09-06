//
//  HistoryItem.h
//  BaconApp
//
//  Created by Jordan on 3/09/11.
//  Copyright 2011 Team Bacon. All rights reserved.
//

#import <Foundation/Foundation.h>


@interface HistoryItem : NSObject
{
    NSString * htmlFile;
}

@property(nonatomic, retain) NSString * htmlFile;

@property(nonatomic) CGPoint coordinates;

@property(nonatomic) int x, y;

-(HistoryItem *) initWithHtmlFile:(NSString *)htmlFile x:(int)x y:(int)y;

@end
