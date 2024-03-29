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
#import "Interpreter.h"
#import "ModalViewController.h"

@interface ScannerViewController : ModalViewController
	<ZBarReaderDelegate>
{
	UIImageView *resultImage;
	UITextView *resultText;
	HistoryItem *current;
	Interpreter *interpreter;
}

@property (nonatomic, retain) IBOutlet UIImageView *resultImage;
@property (nonatomic, retain) IBOutlet UITextView *resultText;
@property (nonatomic, retain) HistoryItem *current;
@property (nonatomic, retain) Interpreter *interpreter;

-(IBAction) scanButtonPressed:(id)sender;
@end
