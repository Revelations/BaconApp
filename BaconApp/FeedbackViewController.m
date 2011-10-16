//
//  FeedbackController.m
//  BaconApp
//
//  Copyright 2011 Team Bacon. All rights reserved.
//

#import "FeedbackViewController.h"
#import "Update.h"
#import "Reachability.h"
#import "BaconAppDelegate.h"

@implementation FeedbackViewController

//----------------------------------------------------------------------------------------------------------
//
//	Misc methods.
//
//----------------------------------------------------------------------------------------------------------

// Adds a custom done button to the on screen keyboard.
//
-(void)addButtonToKeyboard
{
	UIButton * doneButton = [UIButton buttonWithType:UIButtonTypeCustom];
	doneButton.frame = CGRectMake(0, 163, 106, 53);
	doneButton.adjustsImageWhenHighlighted = NO;
	
    [doneButton setImage:[UIImage imageNamed:@"DoneUp.png"] forState:UIControlStateNormal];
    [doneButton setImage:[UIImage imageNamed:@"DoneDown.png"] forState:UIControlStateHighlighted];
	
    [doneButton addTarget:self action:@selector(doneButton:) forControlEvents:UIControlEventTouchUpInside];
	
	UIWindow* tempWindow = [[[UIApplication sharedApplication] windows] objectAtIndex:1];
	UIView* keyboard;
	
    for(int i=0; i<[tempWindow.subviews count]; i++)
    {
		keyboard = [tempWindow.subviews objectAtIndex:i];
		
		// Keyboard found, add the button.
        if([[keyboard description] hasPrefix:@"<UIPeripheralHost"] == YES)
            [keyboard addSubview:doneButton];
    }
}

// Custom event, fired whenever the keyboard is shown.
// Calls above method to add custom done button to keyboard.
//
-(void) keyboardDidShow: (NSNotification *) note
{
    if(displayAdditionalDoneButton)
        [self addButtonToKeyboard];
}

// Called when the custom done button is pressed.
// Resigns control from the number text field, hides keyboard.
//
-(void)doneButton:(id)sender 
{
    [textFieldNumber resignFirstResponder];
}

// Sends feedback to the server.
//
-(IBAction)SendFeedback:(id)sender{
	
	NSString * numbers      = [textFieldNumber text];
	NSString * nationality  = [textFieldNationality text];
	NSString * seen         = [textFieldSighted text];
	NSString * feedback     = [textViewMiscellaneous text];
	
	NSMutableArray * scannedContents = [(BaconAppDelegate *) [[UIApplication sharedApplication] delegate]scannedItems];
	int val_count = [scannedContents count];
	
	NSMutableData *data = [NSMutableData data];
	NSString * newLine = @"%@\r\n";
	[data appendData:[[NSString stringWithFormat:newLine, numbers] dataUsingEncoding:NSUTF8StringEncoding]];    
	[data appendData:[[NSString stringWithFormat:newLine, nationality] dataUsingEncoding:NSUTF8StringEncoding]];
	[data appendData:[[NSString stringWithFormat:newLine, seen] dataUsingEncoding:NSUTF8StringEncoding]];
	[data appendData:[[NSString stringWithFormat:newLine, feedback] dataUsingEncoding:NSUTF8StringEncoding]];
	[data appendData:[[NSString stringWithFormat:newLine, [NSString stringWithFormat:@"%d", val_count]] dataUsingEncoding:NSUTF8StringEncoding]];
	Update * updateSession = [[Update alloc] init];
	
	NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
	NSString *documentsDirectory = [paths objectAtIndex:0];
	
	NSString *filePath = [NSString stringWithFormat:@"%@%@", documentsDirectory, @"/feedback.txt"];
	[data writeToFile:filePath atomically:YES];
	
	// If we have internet, send the file. If not, spawn a thread to wait and send it eventually.
	if([updateSession CheckForInternet] != -1)
		[updateSession uploadPhp:filePath];
	else
		[updateSession spawnThreadForApplication:nil WithPath:filePath WithSleepTime:300 WithType:1];
	
	[updateSession release];
}

//----------------------------------------------------------------------------------------------------------
//
//	UIViewController Methods.
//
//----------------------------------------------------------------------------------------------------------

// Called when the view is finished loading.
//
- (void)viewDidLoad
{
	// Set view title.
	self.navigationItem.title = @"Feedback";
	
    // Hacky method of making a text view look like a text field - Put a disabled text field behind it.
    textFieldMiscellaneous.frame = CGRectMake(20, 233, 281, 132);
    
	// Fire an event when the keyboard shows.
    [[NSNotificationCenter defaultCenter] addObserver:self 
                                             selector:@selector(keyboardDidShow:) 
                                                 name:UIKeyboardDidShowNotification 
                                               object:nil];	
	[super viewDidLoad];
}

// Called when the view is unloaded.
//
- (void)viewDidUnload
{
	[super viewDidUnload];
}

// Called when the orientation changes.
// Controls whether or not the interface should be auto rotated.
//
- (BOOL)shouldAutorotateToInterfaceOrientation:(UIInterfaceOrientation)interfaceOrientation
{
	// Return YES for supported orientations
	return (interfaceOrientation == UIInterfaceOrientationPortrait);
}

- (void)didReceiveMemoryWarning
{
	// Releases the view if it doesn't have a superview.
	[super didReceiveMemoryWarning];
}

- (void)dealloc
{
    [[NSNotificationCenter defaultCenter] removeObserver:self];
	[super dealloc];
}




//----------------------------------------------------------------------------------------------------------
//
//	UITextFieldDelegate Methods
//
//----------------------------------------------------------------------------------------------------------

// Called when a text field is about to be edited.
// If that text field is the group number field, enable the custom done button.
//
-(BOOL)textFieldShouldBeginEditing:(UITextField *)textField
{
    if(textField == textFieldNumber)
        displayAdditionalDoneButton = YES;
    
    return YES;
}

// Called when a text field is finished with.
// If that text field is the group number field, disable the custom done button.
//
-(void)textFieldDidEndEditing:(UITextField *)textField
{
	if(textField == textFieldNumber)
		displayAdditionalDoneButton = NO;
}

// Called when a user hits the done button on a text field keyboard.
// Resigns focus and hides keyboard.
//
- (BOOL)textFieldShouldReturn:(UITextField *)textField
{
	[textField resignFirstResponder];
	return YES;
}

//----------------------------------------------------------------------------------------------------------
//
//	UITextViewDelegate Methods
//
//----------------------------------------------------------------------------------------------------------

// Called when the text view is about to be edited.
// Moves the scroll pane upward so that keyboard is not drawn over top of text view.
//
-(BOOL)textViewShouldBeginEditing:(UITextView *)textView
{
	scrollView.frame = CGRectMake(0, -200, 320, 455);
    return YES;
}

// Called when a text view is finished with.
// Moves the scroll pane back to it's original location.
//
-(void)textViewDidEndEditing:(UITextView *)textView
{
	scrollView.frame = CGRectMake(0, 0, 320, 455);
}

// Called whenever the text changes in a text view.
// If the new text is a return character, user has hit the done button.
// Resign focus and hide keyboard.
//
-(BOOL) textView:(UITextView *)textView shouldChangeTextInRange:(NSRange)range replacementText:(NSString *)text
{
    if([text isEqualToString:@"\n"])
    {
        [textViewMiscellaneous resignFirstResponder];
        return NO;
    }
    return YES;
}

@end
