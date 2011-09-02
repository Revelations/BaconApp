//
//  ViewAppViewController.h
//  ViewApp
//
//  Created by Shii Kayano on 1/09/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface ViewAppViewController : UIViewController {
	UITabBarController	*tbc;
}
-(IBAction)showModalTabBar;
-(void)dismissTabBar;

@property (nonatomic, retain) UITabBarController *tbc;
@end

