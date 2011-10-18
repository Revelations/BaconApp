//
//  GameViewController.m
//  BaconApp
//
//  Created by Donovan Hoffman on 14/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "GameViewController.h"
#import "BaconAppDelegate.h"
#import "AnswerSelectionController.h"
#import "Update.h"
#import "DDFileReader.h"

#include <stdlib.h>

@implementation GameViewController
@synthesize	answersGiven;
@synthesize asc;
#define CONST_Cell_height 44.0f
#define CONST_Cell_width 270.0f

#define CONST_textLabelFontSize     17
#define CONST_detailLabelFontSize   15
static UIFont *subFont;
static UIFont *titleFont;
#pragma mark - HelperMethods

- (UIFont*) TitleFont;
{
	if (!titleFont) titleFont = [UIFont boldSystemFontOfSize:CONST_textLabelFontSize];
	return titleFont;
}

- (UIFont*) SubFont;
{
	if (!subFont) subFont = [UIFont systemFontOfSize:CONST_detailLabelFontSize];
	return subFont;
}

- (UITableViewCell*) CreateMultilinesCell :(NSString*)cellIdentifier
{
	UITableViewCell *cell = [[[UITableViewCell alloc] initWithStyle:UITableViewCellStyleSubtitle 
													reuseIdentifier:cellIdentifier] autorelease];
	
	cell.textLabel.numberOfLines = 0;
	cell.textLabel.font = [self TitleFont];
	
	cell.detailTextLabel.numberOfLines = 0;
	cell.detailTextLabel.font = [self SubFont];
	
	return cell;
}

- (int) heightOfCellWithTitle :(NSString*)titleText 
				   andSubtitle:(NSString*)subtitleText
{
	CGSize titleSize = {0, 0};
	CGSize subtitleSize = {0, 0};
	
	if (titleText && ![titleText isEqualToString:@""]) 
		titleSize = [titleText sizeWithFont:[self TitleFont] 
						  constrainedToSize:CGSizeMake(CONST_Cell_width, 4000) 
							  lineBreakMode:UILineBreakModeWordWrap];
	
	if (subtitleText && ![subtitleText isEqualToString:@""]) 
		subtitleSize = [subtitleText sizeWithFont:[self SubFont] 
								constrainedToSize:CGSizeMake(CONST_Cell_width, 4000) 
									lineBreakMode:UILineBreakModeWordWrap];
	
	return titleSize.height + subtitleSize.height;
}



//reads the file into the private var currentQuestionFiles
-(void) readSingleQuestionFile:(NSString *) filePath{
	NSFileManager *fileManager = [NSFileManager defaultManager];
	NSMutableArray * array = [[NSMutableArray alloc]init];
	//const char * fileName = [filePath UTF8String];
	
		NSLog(@"jim has been lazarused, lookin at %@", filePath);
	if([fileManager fileExistsAtPath: filePath]){
		NSLog(@"about to go into the loop");
		DDFileReader * reader = [[DDFileReader alloc] initWithFilePath:filePath];
		
		
		NSString * line = nil;
		while ((line =[reader readLine])) {			
			NSLog(@"singlequestionfile: currentQCount: %i", [currentQuesOptMutArray count]);
			[currentQuesOptMutArray addObject: [line retain]];
			NSLog(@"singlequestionfile: currentQCount: %i", [currentQuesOptMutArray count]);
			
		}
		[reader release];
	}
	else {
		NSLog(@"file does not exist: %@", filePath);
	}
	[array release];
		
		/*FILE * file = fopen(fileName, "r");
        Interpreter * interpreter = [[[Interpreter alloc] init] autorelease];
		while(!feof(file)){
			for (int i = 0; i < 6; i++) {
				NSString * line = [interpreter readLineAsNSString:file];
				[array addObject: line];
			}
			[currentQuestionFiles addObject: array];
		}
	}
	[array release];*/
}

-(void) readQuestionFiles{
	//NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
	//NSString *documentsDirectory = [paths objectAtIndex:0];
//	NSString *localGameFilesDirectory = [Update localGameDir];
	NSFileManager *fileManager = [NSFileManager defaultManager];
	BaconAppDelegate * appDelegate = (BaconAppDelegate *)[[UIApplication sharedApplication] delegate];
	NSMutableArray * scannedCodes = [appDelegate scannedItems];
	
	//	NSString *logFilePath = [NSString stringWithFormat:@"%@/%@", localGameFilesDirectory, @"log.txt"];
	
	if([fileManager fileExistsAtPath:[Update localGameDir] isDirectory:NULL]){
		if (!scannedCodes) {	NSLog(@"Scanned codes is nil in readQuestionFiles"); }

		NSLog(@"gameDir: %@ exists, scanned code count is %i", [Update localGameDir], [scannedCodes count]);
		
		for (int i = 0; i < [scannedCodes count]; i++) {
			NSString * currentCode = [scannedCodes objectAtIndex:i];
			NSString * filePath = [NSString stringWithFormat:@"%@/%@.ques", [Update localGameDir],currentCode];
			[self readSingleQuestionFile: filePath];
		}
	}
}


-(NSArray *) generateRandomQuestionIndices: (NSMutableArray *) pool{
	NSLog(@"hello start of of generateRandomQuestionIndices pool: %@", pool);
	NSLog(@"Pool count: %i ", [pool count]);
	int maxQuizSize = 10;// max size of the quiz
	NSMutableArray *tmp = [NSMutableArray new];// array of ordered indices
	NSMutableArray *result = [NSMutableArray new];
	//temporarily store all indexes for random
	if (maxQuizSize <= [pool count]) {
		// Fill tmp with ints, ordered.
		for (int k = 0; k < [pool count]; k++) {
			[tmp addObject: [NSNumber numberWithInt:k]];
		}
		
		//generates a random for the index of the selected random question
		for (int i = 0; i < maxQuizSize; i++) {
			int randNumber = arc4random() % [tmp count];
			NSNumber * randQuestion  = [tmp objectAtIndex: randNumber]; // Grab a number from the question index pool
			[result addObject: [pool objectAtIndex:[randQuestion intValue]]];
			[tmp removeObjectAtIndex:[randQuestion intValue]];
		}NSLog(@"End of generate random questions: %@", result);
		[tmp release];
		
		
		return [NSArray arrayWithArray:result];
	}
	else {
		
		[tmp release];
		[result release];
		return [NSArray arrayWithArray: pool];
	}
}

//initializes all the questions
-(void) initGame {
	currentQuesOptMutArray = [NSMutableArray new];
	//quizQuestions = [NSMutableArray new];//STORES THE WHOLE Q&A FILE
	data = [NSMutableArray new];

	//need to iterate through the list of scanned codes
	int quesSize = 6;//size of each q, lines = 6
	
	[self readQuestionFiles];
	NSLog(@"Question files count :%i", [currentQuesOptMutArray count]);
	int numOfQuestions = [currentQuesOptMutArray count] / quesSize; // 3 if count == 18 (which is 6 * 3, ofc);
	NSMutableArray * quesBlocks = [NSMutableArray new];
	
	for (int i = 0; i < [currentQuesOptMutArray count]; i++) {
		NSLog(@"question[%i] is:", i);
		NSArray * quesBlock;
		NSString * question, *correctOption, *optionA, *optionB,*optionC, *optionD;
		question = [[currentQuesOptMutArray objectAtIndex:i++]retain];
		optionA = [[currentQuesOptMutArray objectAtIndex:i++] retain];
		optionB = [[currentQuesOptMutArray objectAtIndex:i++] retain];
		optionC = [[currentQuesOptMutArray objectAtIndex:i++] retain];
		optionD = [[currentQuesOptMutArray objectAtIndex:i++] retain];
		correctOption = [[currentQuesOptMutArray objectAtIndex:i] retain];

		NSLog(@"currentOPtArray: %@", currentQuesOptMutArray);
		quesBlock = [NSArray arrayWithObjects:
					 question,
					 optionA,
					 optionB,
					 optionC,
					 optionD,
					 correctOption,
					 nil];

		[quesBlocks addObject: quesBlock];
		NSLog(@"QuestBlock[0]R %@", [quesBlocks objectAtIndex:0]);
	}
	
	// array of selected questions
	quizQuestions = [[self generateRandomQuestionIndices: quesBlocks] retain];
}
-(NSArray*)getQuestions{
	NSLog(@"Getting questions. quizQuestions count = %i", [quizQuestions count]);
	NSMutableArray * returnArray = [NSMutableArray new];
	
	for (int i = 0; i < [quizQuestions count]; i++) {
		NSLog(@"adding to getQuestions: %@ with size %i/%i",
			  [[quizQuestions objectAtIndex:i] objectAtIndex:0], i, [returnArray count]);
		
		NSLog(@" %@ is at quizQuestions %i", [quizQuestions objectAtIndex:i], i);
		[returnArray addObject: [[quizQuestions objectAtIndex: i] objectAtIndex:0]];
		
		NSLog(@"quizQuestions %@", quizQuestions);
	}
	return [NSArray arrayWithArray:returnArray];

}

-(NSArray*)getOptions {
	NSLog(@"Getting options. I can has !");
	NSMutableArray *returnArray = [NSMutableArray new];
	
	for (int i = 0; i < [quizQuestions count]; i++) { // Loops over
		[returnArray addObject:[[quizQuestions objectAtIndex:i]objectAtIndex:1]];
		[returnArray addObject:[[quizQuestions objectAtIndex:i]objectAtIndex:2]];
		[returnArray addObject:[[quizQuestions objectAtIndex:i]objectAtIndex:3]];
		[returnArray addObject:[[quizQuestions objectAtIndex:i]objectAtIndex:4]];
	}
	
	assert(([returnArray count] %4) == 0);
	return [[NSArray arrayWithArray:returnArray] retain];

}

-(NSArray*)getAnswers{
	NSLog(@"Getting answers. I can has answers? ");

	NSMutableArray * returnArray = [NSMutableArray new];
	
	for (int i = 0; i < [quizQuestions count]; i++) {
		[returnArray addObject: [[quizQuestions objectAtIndex: i]objectAtIndex:5]];
	}
	return [[NSArray arrayWithArray:returnArray] retain];
}


- (void)didReceiveMemoryWarning
{
	// Releases the view if it doesn't have a superview.
	[super didReceiveMemoryWarning];
	
	// Release any cached data, images, etc that aren't in use.
}

-(NSArray *) formatOptionsForView{
	NSArray * options = [self getOptions];
	NSMutableArray * returnArray = [[NSMutableArray new]autorelease];
	for (int i = 0; i < [options count];) {
		NSString * optionA = [options objectAtIndex:i++];
		NSString * optionB = [options objectAtIndex:i++];
		NSString * optionC = [options objectAtIndex:i++];
		NSString * optionD = [options objectAtIndex:i++];
		
		NSString *optionTmp = [NSString stringWithFormat:@"1:%@\r\n2:%@\r\n3:%@\r\n4:%@", optionA, optionB, optionC, optionD];
		[returnArray addObject: optionTmp];
	}
	return [[NSArray arrayWithArray:returnArray] retain];
}

//static NSArray *titles;
//static NSArray *subtitles;

#pragma mark - View lifecycle
- (void)viewDidLoad
{
	[super viewDidLoad];
	titles =  [[[NSArray alloc]init] retain];
	subtitles = [[[NSArray alloc]init] retain];
	[self initGame];
	
	tableView = [[UITableView alloc]initWithFrame:CGRectMake(0, 0, 320, 410) style:UITableViewStylePlain];
	[tableView setDataSource:self];
	[tableView setDelegate:self];
	[self.view addSubview:tableView];
	//[table release];
	
/*BaconAppDelegate * delgato = (BaconAppDelegate *)[[UIApplication sharedApplication] delegate];
	delgato.answersGiven = [NSMutableArray arrayWithArray: titles];*/

	
	//answersGiven = [NSArray arrayWithArray:titles];
		NSLog(@"setting the titles");
		titles = [[NSArray arrayWithArray: [self getQuestions]] retain];
	//NSLog(@"titles [0]:%@ [1]:%@", [titles objectAtIndex: 0], [titles objectAtIndex: 1]);
	NSLog(@"finished the titles");
	   /*[[NSArray arrayWithObjects:
				   @"Shakespeare's Sonnet 1: From Fairest Creatures We Desire Increase",
				   @"Shakespeare's Sonnet 2: When Forty Winters Shall Besiege Thy Brow",
				   @"Shakespeare's Sonnet 3: Look In Thy Glass, And Tell The Face Thous Viewest",
				   nil] retain];*/
		NSLog(@"setting the subtitles");
		subtitles = [[NSArray arrayWithArray: [self formatOptionsForView]] retain];
		NSLog(@"finished the subtitles");
	
	/*[[NSArray arrayWithObjects: 
					  @"We want all beautiful creatures to reproduce themselves so that beauty’s flower will not die out; but as an old man dies in time, he leaves a young heir to carry on his memory.",
					  @"When forty winters have attacked your brow and wrinkled your beautiful skin, the pride and impressiveness of your youth, so much admired by everyone now, will be have become a worthless, tattered weed.",
					  @"Look in your mirror and tell the face you see that it's time it should create another If you do not renew yourself you would be depriving the world, and stop some woman from becoming a mother.",
					  nil] retain];*/

	self.navigationItem.title = @"Trivia Quiz";
	
		
	NSLog(@"my name is :%@", [[titles retain] objectAtIndex:0]);
	
	
	// Uncomment the following line to display an Edit button in the navigation bar for this view controller.
	// self.navigationItem.rightBarButtonItem = self.editButtonItem;
}

- (void)viewDidUnload
{
	[super viewDidUnload];
	// Release any retained subviews of the main view.
	// e.g. self.myOutlet = nil;
}

- (void)viewWillAppear:(BOOL)animated
{
	[super viewWillAppear:animated];
}

- (void)viewDidAppear:(BOOL)animated
{
	[super viewDidAppear:animated];
}

- (void)viewWillDisappear:(BOOL)animated
{
	[super viewWillDisappear:animated];
}

- (void)viewDidDisappear:(BOOL)animated
{
	[super viewDidDisappear:animated];
}

- (BOOL)shouldAutorotateToInterfaceOrientation:(UIInterfaceOrientation)interfaceOrientation
{
	// Return YES for supported orientations
	return (interfaceOrientation == UIInterfaceOrientationPortrait);
}


#pragma mark - table delegate methods
- (NSInteger)numberOfSectionsInTableView:(UITableView *)tableView {
	return 2;
}

- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section {
	//return 1;
	//NSLog(@"configuring the # sections");
	if (section == 0) {
	 return MIN([titles count], [subtitles count]);
	}
	else return 1;
}

- (UITableViewCell *)tableView:(UITableView *)tView 
		 cellForRowAtIndexPath:(NSIndexPath *)indexPath {
	NSLog(@"Started configuring cells");
    
    static NSString *CellIdentifier = @"Cell";
    
    UITableViewCell *cell = [tView dequeueReusableCellWithIdentifier:CellIdentifier];
    if (cell == nil) {
		if (indexPath.section == 0) {
			cell = [self CreateMultilinesCell:CellIdentifier];
		}
		else {
			cell = [[[UITableViewCell alloc] initWithFrame:CGRectZero reuseIdentifier:CellIdentifier] autorelease];
		}

    }
	
	if(indexPath.section == 1) {
		switch(indexPath.row) {
			case 0: [cell.textLabel setText:@"Finished!"]; break;
		}
	}
	else {
		// do whatever for the other sections
		cell.textLabel.text = [titles objectAtIndex:indexPath.row];
		cell.detailTextLabel.text = [subtitles objectAtIndex:indexPath.row];
	}
	NSLog(@"finished configuring cells");
    return cell;
}

-(CGFloat)tableView:(UITableView *)tableView heightForRowAtIndexPath:(NSIndexPath *)indexPath
{
	NSLog(@"Titles has %i items", [titles count]);
	
	NSString *title = [titles objectAtIndex:indexPath.row];
	NSString *subtitle = [subtitles objectAtIndex:indexPath.row];
	
	int height = 10 + [self heightOfCellWithTitle:title andSubtitle:subtitle];
	return (height < CONST_Cell_height ? CONST_Cell_height : height);
}

-(void) doMark{
	BaconAppDelegate * delgato = (BaconAppDelegate *)[[UIApplication sharedApplication] delegate];
	NSArray * answersToMark = [NSArray arrayWithArray:delgato.answersGiven];//answers to mark
	NSArray * answers  = [NSArray arrayWithArray:[self getAnswers]];//correct answers
	NSLog(@"Answers: %@", answers);
	int correct = 0;
	int incorrect = 0;
	int numOfQuestions = [[self getQuestions]count];
	NSLog(@"answersToMarkCount : %i", [answersToMark count]);
	if ([answersToMark count]) {
		for (int i = ([answersToMark count]-1); i >= 0; i--) {
			//answer is the unparsed answer
			NSString * answer = [answersToMark objectAtIndex:i];

			NSArray * tmp = [answer componentsSeparatedByString:@"a"];
			
			NSString * befuddledQuestionIndex = [tmp objectAtIndex:0];
			int questionIndex = [[befuddledQuestionIndex substringWithRange:NSMakeRange(1, ([befuddledQuestionIndex length] -1))]intValue];
			NSNumber * answerGiven = [NSNumber numberWithInt:[[tmp objectAtIndex:1] intValue]];
			NSLog(@"Jim");
			NSString * answerCheck = [[answers objectAtIndex:questionIndex] stringByReplacingOccurrencesOfString:@"\n" withString:@""]; // Shii says: You typed it wrong :D withString: vs With:
	//		NSRange * range = NSMakeRange(0, [answerCheck length] -2);
			NSLog (@"answer: %i", [answerCheck intValue]);
			NSLog(@"given: %i", [answerGiven intValue]);
			if([answerCheck intValue] == [answerGiven intValue]){
				
				correct++;
			}
			else {
				incorrect++;
			}
			if((correct + incorrect) == numOfQuestions)
				break;

		}
	}
	[answersGiven release];	
	answersGiven = [NSMutableArray new];
		UIAlertView *alert = [[UIAlertView alloc] initWithTitle:@"Marked!" message:[NSString stringWithFormat:@" You got %i out of %i right!", correct, numOfQuestions]
													   delegate:self cancelButtonTitle:@"Ok" otherButtonTitles:nil];
		[alert show];
		[alert release];
}


-(void)tableView:(UITableView *)aTableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath{
	
	[aTableView deselectRowAtIndexPath:indexPath animated:YES];
	
	if (indexPath.section == 0) {
		AnswerSelectionController * viewController;
		if (self.asc == nil) {
			viewController = [[[AnswerSelectionController alloc] initWithNibName:@"AnswerSelectionController" bundle:nil]retain];
		}
		else {
			viewController = [self.asc retain];
			assert(viewController != nil);
		}

			NSLog(@"Row: %i", indexPath.row);
			[viewController setQuestion: [NSNumber numberWithInt:indexPath.row]];
			NSLog(@"Using question: %i", [viewController.question intValue]);
			viewController.my_parent = self;
			[self setAsc: viewController];
			//currentViewController = viewController;
			// Cleanup resources
			
		[[self navigationController] pushViewController:asc animated:YES];
		[viewController release];
	}
	else {
		NSLog(@"Section 2");
		[self doMark];
	}
	
	// Create the modal view controller
	//[aTableView deselectRowAtIndexPath:indexPath animated:YES];
}
- (void)dealloc
{
	[titles release];
	[subtitles release];
	[super dealloc];
}




@end