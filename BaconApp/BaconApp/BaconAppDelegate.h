//
//  BaconAppDelegate.h
//  BaconApp
//
//  Created by Donoan Hoffman on 6/09/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "DataModel.h"


@interface BaconAppDelegate : NSObject
	<UIApplicationDelegate, UITabBarDelegate>
{
	UIWindow *window;
	UITabBarController *tabBarController;
	DataModel *model;
}

@property (nonatomic, retain) IBOutlet UIWindow *window;
@property (nonatomic, retain) IBOutlet UITabBarController *tabBarController;

@property (nonatomic, retain) DataModel *model;

@end
