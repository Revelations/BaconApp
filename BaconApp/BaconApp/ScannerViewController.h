//
//  ScannerViewController.h
//  BaconApp
//
//  Created by Donoan Hoffman on 6/09/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "HistoryItem.h"
#import "DataModel.h"

@interface ScannerViewController : UIViewController
	<ZBarReaderDelegate>
{
	UIImageView *resultImage;
	UITextView *resultText;
	
}

@property (nonatomic, retain) IBOutlet UIImageView *resultImage;
@property (nonatomic, retain) IBOutlet UITextView *resultText;

-(IBAction) scanButtonPressed;
- (void) webViewLoadPage:(NSString *)path;
@end
