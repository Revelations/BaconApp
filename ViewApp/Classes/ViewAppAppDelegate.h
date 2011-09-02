//
//  ViewAppAppDelegate.h
//  ViewApp
//
//  Created by Shii Kayano on 1/09/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>

@class ViewAppViewController;

@interface ViewAppAppDelegate : NSObject <UIApplicationDelegate> {
    UIWindow *window;
    ViewAppViewController *viewController;
}

@property (nonatomic, retain) IBOutlet UIWindow *window;
@property (nonatomic, retain) IBOutlet ViewAppViewController *viewController;

@end

