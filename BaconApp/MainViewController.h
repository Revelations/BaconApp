//
//  MainViewController.h
//  BaconApp
//
//  Created by Jordan on 31/08/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>


@interface MainViewController : UIViewController<UIWebViewDelegate>
{
    IBOutlet UIWebView * webview;
    IBOutlet UIBarButtonItem * scanButton;
    IBOutlet UIBarButtonItem * mapButton;
    IBOutlet NSString * menuPath;
}

// Hard coded main menu html path. Sets the initial page to load on app start.
extern NSString * const MENU_FILE_PATH;

// Main browser control used to display information.
@property(nonatomic, retain) UIWebView * webView;

// Button to initiate barcode scan.
@property(nonatomic, retain) UIBarButtonItem * scanButton;

// Toggle button for switching between map/browser.
@property(nonatomic, retain) UIBarButtonItem * mapButton;

//
-(IBAction) loadFile:(NSString *)filePath WithSender:(id) sender;

//
-(IBAction) back;

//
-(IBAction) forward;

@end
