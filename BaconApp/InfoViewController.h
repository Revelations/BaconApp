//
//  InfoViewController.h
//  BaconApp
//
//  Created by Donoan Hoffman on 6/09/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "DataModel.h"


@interface InfoViewController : UIViewController {
	UIWebView *webView;
//	DataModel *model;
	UIActivityIndicatorView *activityIndicator;
	bool loadingMapScreen;
}

@property (nonatomic, retain) UIWebView *webView;
//@property (nonatomic, retain) DataModel *model;
@property (nonatomic, retain) UIActivityIndicatorView *activityIndicator;
@property (nonatomic) bool loadingInformation;

@end
