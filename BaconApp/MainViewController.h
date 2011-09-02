//
//  BaconAppAppDelegate.h
//  BaconApp
//
//  Created by Jordan on 31/08/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface MainViewController : UIViewController <ZBarReaderDelegate>
{
    IBOutlet UIWebView * webview;
    IBOutlet UIBarButtonItem * scanButton;
    IBOutlet UIBarButtonItem * mapButton;
    IBOutlet NSString * menuPath;
    IBOutlet UITextField * resultText;
}

extern NSString * const MENU_HTML_PATH;

@property (nonatomic, retain) IBOutlet UIWindow *window;

// Main browser control used to display information.
@property(nonatomic, retain) UIWebView * webView;

// Button to initiate barcode scan.
@property(nonatomic, retain) UIBarButtonItem * scanButton;

// Toggle button for switching between map/browser.
@property(nonatomic, retain) UIBarButtonItem * mapButton;

@property(nonatomic, retain) IBOutlet UITextField * resultText;


-(IBAction) scanButtonPressed;
-(IBAction) mapButtonPressed;




@end
