//
//  MainViewController.m
//  BaconApp
//
//  Created by Jordan on 31/08/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "MainViewController.h"



@implementation MainViewController

@synthesize webView, scanButton, mapButton;

// Define the menu path.
NSString * const MENU_FILE_PATH = @"Menu.html";

// Called when the UIWebview is initialized.
// Loads the main menu html file in the browser.
-(void) viewDidLoad
{
    [super viewDidLoad];
    
    NSString * stringFilePath = MENU_FILE_PATH;
    
    [self loadFile:MENU_FILE_PATH WithSender:nil];
}


// Loads a file in the browser from a file path string.
-(IBAction) loadFile:(NSString *)filePath WithSender:(id)sender
{
    NSURL * url = [NSURL fileURLWithPath:filePath];
    NSURLRequest * request = [NSURLRequest requestWithURL:url];
    
    [webview loadRequest:request];
}

-(IBAction) back
{
    [webview goBack];
}

-(IBAction) forward
{
    [webview goForward];
}

@end
