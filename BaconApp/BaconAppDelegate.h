//
//  BaconAppDelegate.h
//  BaconApp
//
//  Created by Donoan Hoffman on 6/09/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "DataModel.h"

// Name (without extension) of the main menu html page, loaded on app start.
extern NSString * const MENU_HTML_FILE;

// Name (without extension) of the map html page.
extern NSString * const MAP_HTML_FILE;

// Here is where all our webpages reside.
extern NSString * const WEB_DIRECTORY;

@interface BaconAppDelegate : NSObject
	<UIApplicationDelegate, UITabBarDelegate>
{
	UIWindow *window;
	UITabBarController *tabBarController;
	DataModel *model;
	int x;
	int y;
	NSString *html;
    NSMutableArray * scanNum;
}


@property (nonatomic, retain) IBOutlet UIWindow *window;
@property (nonatomic, retain) IBOutlet UITabBarController *tabBarController;
@property (nonatomic, retain) IBOutlet UIView *currentView;
//@property (nonatomic, retain) IBOutlet UpdateController *tempTestView;

@property (nonatomic, retain) NSMutableArray* scannedItems;
@property (nonatomic, retain) DataModel *model;
@property (readwrite) int x;
@property (readwrite) int y;
@property (nonatomic, retain) NSString * html;
@property (readwrite) BOOL update;// a property to test if the app has already asked to update


@end
