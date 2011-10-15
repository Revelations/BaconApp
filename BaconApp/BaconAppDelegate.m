//
//  BaconAppDelegate.m
//  BaconApp
//
//  Created by Donoan Hoffman on 6/09/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "BaconAppDelegate.h"
#import "InfoViewController.h"
#import "MapViewController.h"
#import "DataModel.h"
#import "UpdateController.h"
#import "Update.h"
#import "Reachability.h"

#import "NavigationView.h"
// Name (without extension) of the main menu html page, loaded on app start.
NSString * const MENU_HTML_FILE = @"menu";
// Name (without extension) of the map html page.
NSString * const MAP_HTML_FILE = @"map";

// Here is where all our webpages reside.
NSString * const WEB_DIRECTORY = @"Web";
NSString * const GAME_DIRECTORY = @"Game";
NSString * const CONTENT_DIRECTORY = @"Content";

@implementation BaconAppDelegate

@synthesize window;
@synthesize tabBarController;
@synthesize model;
@synthesize x, y, html;
@synthesize update;
@synthesize scannedItems = _scannedItems;
@synthesize currentView = _currentView;
@synthesize serverIpAddress = _serverIpAddress;
@synthesize fontSize = _fontSize;

#pragma mark - helper methods
#pragma mark Application lifecycle

- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions {
    
    update = NO;
	
	NSLog(@"Hello World, MainView did finish launching");
    NSLog(@"earth to the world of jim...");
	
    // Override point for customization after application launch.
    self.window.rootViewController = [NavigationView new];
	
    [self.window makeKeyAndVisible];
	
    return YES;
}

-(void)readsettingsfile{
	/*if(settings file exists){
	 //readline from file
	 //split on the |
	 //first part is the font size [0]=fontSize;
	 //second part is serveripaddress [1] = serverIpAddress;
	 //close the file
	 }*/
	NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
    NSString *documentsDirectory = [paths objectAtIndex:0];
	NSString *s = @"settings.txt";
	NSString *filePath = [NSString stringWithFormat:@"%@%@%@", documentsDirectory,@"/", s];
	NSFileManager *fileManager = [NSFileManager defaultManager];
	if ([fileManager fileExistsAtPath:filePath]) {
		NSString *line = [NSString stringWithContentsOfFile:filePath encoding:NSUTF8StringEncoding error:nil];
		
		NSArray *parts = [line componentsSeparatedByString: @"|"]; 
		serverIpAddress = [parts objectAtIndex:1];
		fontSize = [parts objectAtIndex:0];
	}
	
}

- (void)applicationWillResignActive:(UIApplication *)application {
    /*
     Sent when the application is about to move from active to inactive state. This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) or when the user quits the application and it begins the transition to the background state.
     Use this method to pause ongoing tasks, disable timers, and throttle down OpenGL ES frame rates. Games should use this method to pause the game.
     */
}


- (void)applicationDidEnterBackground:(UIApplication *)application {
    
    
    NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
    NSString *documentsDirectory = [paths objectAtIndex:0];
    Update * updateSession = [[Update alloc]init];
    
    NSString *filePath = [NSString stringWithFormat:@"%@%@", documentsDirectory, @"/feedback.txt"];
    NSFileManager *fileManager = [NSFileManager defaultManager];
    if([fileManager fileExistsAtPath:filePath]){
        if([updateSession CheckForInternet] != -1){
            [updateSession uploadPhp:filePath];
        }
        else{
			[updateSession spawnThreadForApplication:application WithPath:filePath WithSleepTime:60 WithType:0];
            
           /* //spawns the thread to send feedback
            dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, (int)NULL), ^{
                while ([application backgroundTimeRemaining] > 5.0) {
                    if([updateSession CheckForInternet:receptionCheck] != -1)
                        break;
                    else
                        sleep(60);
                }
                [updateSession uploadPhp:filePath];
            });*/
        }
    }
    
    
    /*
     Use this method to release shared resources, save user data, invalidate timers, and store enough application state information to restore your application to its current state in case it is terminated later. 
     If your application supports background execution, called instead of applicationWillTerminate: when the user quits.
     */
}


- (void)applicationWillEnterForeground:(UIApplication *)application {
    
    
    /*
     Called as part of  transition from the background to the inactive state: here you can undo many of the changes made on entering the background.
     */
}


- (void)applicationDidBecomeActive:(UIApplication *)application {
    
    NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
    NSString *documentsDirectory = [paths objectAtIndex:0];
    Update * updateSession = [[Update alloc]init];
    Reachability * receptionCheck = [[Reachability alloc] init];
    
    NSString *filePath = [NSString stringWithFormat:@"%@%@", documentsDirectory, @"/feedback.txt"];
    NSFileManager *fileManager = [NSFileManager defaultManager];
    if([fileManager fileExistsAtPath:filePath]){
        if([updateSession CheckForInternet] != -1){
            [updateSession uploadPhp:filePath];
        }
        else{
            
            //spawns the thread to send feedback
			[updateSession spawnThreadForApplication:application WithPath:filePath WithSleepTime:300 WithType:1];
            /*dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, (int)NULL), ^{
                while (YES) {
                    if([updateSession CheckForInternet:receptionCheck] != -1)
                        break;
                    else
                        sleep(300);
                }
                [updateSession uploadPhp:filePath];
            });*/
        }
    }
    
    /*
     Restart any tasks that were paused (or not yet started) while the application was inactive. If the application was previously in the background, optionally refresh the user interface.
     */
}


- (void)applicationWillTerminate:(UIApplication *)application {
    /*
     Called when the application is about to terminate.
     See also applicationDidEnterBackground:.
     */
}


#pragma mark -
#pragma mark Memory management

- (void)applicationDidReceiveMemoryWarning:(UIApplication *)application {
    /*
     Free up as much memory as possible by purging cached data objects that can be recreated (or reloaded from disk) later.
     */
}


- (void)dealloc {
	[tabBarController release];
    [window release];
    [super dealloc];
}


@end
