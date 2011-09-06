//
//  BaconAppAppDelegate.h
//  BaconApp
//
//  Created by Jordan on 31/08/11.
//  Copyright 2011 Team Bacon. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "Scanner.h"
#import "Interpreter.h"
#import "HistoryItem.h"

@interface MainViewController : UIViewController <UIWebViewDelegate>
{
    IBOutlet UIWebView * webview;
    IBOutlet UIBarButtonItem * scanButton;
    IBOutlet UIBarButtonItem * mapButton;
    IBOutlet NSString * menuPath;
    IBOutlet UITextField * resultText;
    IBOutlet UIActivityIndicatorView * activityIndicator;
    
    NSMutableArray * history;
    HistoryItem * current;
    
    Scanner * scanner;
    Interpreter * interpreter;
    
    bool loadingMapScreen;
    
    NSString * jScript;
}

// Main app window.
@property (nonatomic, retain) IBOutlet UIWindow * window;

// Main browser control used to display information.
@property(nonatomic, retain) IBOutlet UIWebView * webView;

// Button to initiate barcode scan.
@property(nonatomic, retain) IBOutlet UIBarButtonItem * scanButton;

// Toggle button for switching between map/browser.
@property(nonatomic, retain) IBOutlet UIBarButtonItem * mapButton;

// Text field for testing ZBar lib output.
@property(nonatomic, retain) IBOutlet UITextField * resultText;

// ZBar reader delegate.
@property(nonatomic, retain) Scanner * scanner;

// Interpreter for making sense of scanner data.
@property(nonatomic, retain) Interpreter * interpreter;

@property(nonatomic, retain) NSMutableArray * history;

@property(nonatomic, retain) HistoryItem * current;

// Occurs when user taps scan button.
-(IBAction) scanButtonPressed;

// Occurs when user taps map button.
-(IBAction) mapButtonPressed;


// Loads a given html file into the UIWebView.
-(void) webViewLoadPage:(NSString *) inputString;


@end
