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

@interface MapViewController : UIViewController {
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

@end
