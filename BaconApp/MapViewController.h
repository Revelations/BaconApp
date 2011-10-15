//
//  MapViewController.h
//  BaconApp
//
//  Created by Donoan Hoffman on 6/09/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "Scanner.h"
#import "Interpreter.h"
#import "HistoryItem.h"

@interface MapViewController : UIViewController <UIWebViewDelegate> {
	IBOutlet UIWebView * webView;
	IBOutlet NSString * menuPath;
	IBOutlet UIActivityIndicatorView * activityIndicator;
	
	NSMutableArray * history;
	HistoryItem * current;
	
	Scanner * scanner;
	Interpreter * interpreter;
	
	bool loadingMapScreen;
	
	NSString * jScript;
}

@property (nonatomic, retain) IBOutlet UIWebView *webView;

@end
