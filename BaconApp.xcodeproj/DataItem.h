//
//  codeData.h
//  BaconApp
//
//  Created by Jordan on 1/09/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>


@interface DataItem : NSObject
{
    Point mapCoordinates;
    NSString * htmlPath;
}

@property(nonatomic) Point mapCoordinates;
@property(nonatomic, retain) NSString * htmlPath;

@end
