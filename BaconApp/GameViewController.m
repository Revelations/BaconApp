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
	const char * fileName = [filePath UTF8String];
	
	if([fileManager fileExistsAtPath: filePath]){
		FILE * file = fopen(fileName, "r");
		while(!feof(file)){
			for (int i = 0; i < 6; i++) {
				NSString * line = readLineAsNSString(file);
				[array addObject: line];
			}
			[currentQuestionFiles addObject: array];
		}
	}
}

-(void) readQuestionFiles{
	NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
	NSString *documentsDirectory = [paths objectAtIndex:0];
	NSString *localGameFilesDirectory = [NSString stringWithFormat: @"%@/%@", documentsDirectory, GAME_DIRECTORY];
	NSFileManager *fileManager = [NSFileManager defaultManager];
	BaconAppDelegate * appDelegate = (BaconAppDelegate *)[[UIApplication sharedApplication] delegate];
	NSMutableArray * scannedCodes = [appDelegate scannedItems];
	
	//	NSString *logFilePath = [NSString stringWithFormat:@"%@/%@", localGameFilesDirectory, @"log.txt"];
	
	if([fileManager fileExistsAtPath:localGameFilesDirectory isDirectory:NULL]){
		for (int i = 0; i < [scannedCodes count]; i++) {
			NSString * currentCode = [scannedCodes objectAtIndex:i];
			NSString * filePath = [NSString stringWithFormat:@"%@%@.html", localGameFilesDirectory,currentCode];
			[self readSingleQuestionFile: filePath];
		}
	}
}

//initializes all the questions
-(void) initGame{
	//need to iterate through the list of scanned codes
	int quesSize = 6;
	[self readQuestionFiles];
	
	for (int i = 0; i < [currentQuestionFiles count]; i++) {
		NSMutableArray * currentQ = [currentQuestionFiles objectAtIndex:i];
		
		//need to generate a random number for a question
		int offset =  arc4random_uniform([currentQ count] / quesSize);
		int index = 0;
		
		
		//get the right index for the randomly generated question
		for(; index < offset; index+=quesSize){} 
		
		NSMutableArray *tmp = [[NSMutableArray alloc]init];
		
		//populate the quiz with the new question
		for(int k = 0; k < quesSize; k++){
			[tmp addObject: [currentQ objectAtIndex:index++]];
		}
		[quizQuestions addObject:tmp];
	}
	
	//need to display the relevant questions
	//need to wait for user input
	//need to mark input
	//need to show results
	//allow repeats somehow at least for diagnostics
}
-(NSMutableArray*)getQuestions{
	NSMutableArray * returnArray = [NSMutableArray new];
	
	for (int i = 0; i < [quizQuestions count]; i+=6) {
		[returnArray addObject: [quizQuestions objectAtIndex: i]];
	}
	return returnArray;
}

-(NSMutableArray*)getOptions{
	NSMutableArray *returnArray = [NSMutableArray new];
	
	for (int i = 0; i < [quizQuestions count]; ++i) {
		for (; (i % 4) != 0; i ++) {
			[returnArray addObject: [quizQuestions objectAtIndex: i]];
		}
	}
	return returnArray;
}

-(NSMutableArray*)getAnswers{
	NSMutableArray *returnArray = [NSMutableArray new];
	
	for (int i = 5; i < [quizQuestions count]; i+=5) {
		[returnArray addObject: [quizQuestions objectAtIndex: i]];
	}
	return returnArray;
}

#pragma mark  - App Admin
- (id)initWithStyle:(UITableViewStyle)style
{
	self = [super initWithStyle:style];
	if (self) {
		// Custom initialization
	}
	return self;
}

- (void)dealloc
{
	[super dealloc];
}

- (void)didReceiveMemoryWarning
{
	// Releases the view if it doesn't have a superview.
	[super didReceiveMemoryWarning];
	
	// Release any cached data, images, etc that aren't in use.
}

static NSArray *titles;
static NSArray *subtitles;
#pragma mark - View lifecycle
- (void)viewDidLoad
{
	[self initGame];
	tableView = [[UITableView alloc]initWithFrame:CGRectMake(0, 0, 320, 460) style:UITableViewStylePlain];
	[tableView setDataSource:self];
	[tableView setDelegate:self];
	[self.view addSubview:tableView];
	//[table release];
	
	[super viewDidLoad];
	
	if (!titles)
		titles = /*[arrayWithArray [self getQuestions]];*/
	   [[NSArray arrayWithObjects:
				   @"Shakespeare's Sonnet 1: From Fairest Creatures We Desire Increase",
				   @"Shakespeare's Sonnet 2: When Forty Winters Shall Besiege Thy Brow",
				   @"Shakespeare's Sonnet 3: Look In Thy Glass, And Tell The Face Thous Viewest",
				   nil] retain];
	if (!subtitles)
		subtitles = /*[arrayWithArray [self getOptions]];*/
	
	[[NSArray arrayWithObjects: 
					  @"We want all beautiful creatures to reproduce themselves so that beauty’s flower will not die out; but as an old man dies in time, he leaves a young heir to carry on his memory.",
					  @"When forty winters have attacked your brow and wrinkled your beautiful skin, the pride and impressiveness of your youth, so much admired by everyone now, will be have become a worthless, tattered weed.",
					  @"Look in your mirror and tell the face you see that it's time it should create another If you do not renew yourself you would be depriving the world, and stop some woman from becoming a mother.",
					  nil] retain];

	self.navigationItem.title = @"BACON!";
	
	// Uncomment the following line to preserve selection between presentations.
	// self.clearsSelectionOnViewWillAppear = NO;
											   self.navigationItem.rightBarButtonItem = [[[UIBarButtonItem alloc]
																						  initWithBarButtonSystemItem:UIBarButtonSystemItemDone
																						  target:self
																						  action:@selector(dismissView:)] autorelease];
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
	return 1;
}

- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section {
	
	 return MIN([titles count], [subtitles count]);
}
- (UITableViewCell *)tableView:(UITableView *)tableView 
		 cellForRowAtIndexPath:(NSIndexPath *)indexPath {
    
    static NSString *CellIdentifier = @"Cell";
    
    UITableViewCell *cell = [tableView dequeueReusableCellWithIdentifier:CellIdentifier];
    if (cell == nil) {
        cell = [self CreateMultilinesCell:CellIdentifier];
    }
    
    cell.textLabel.text = [titles objectAtIndex:indexPath.row];
	cell.detailTextLabel.text = [subtitles objectAtIndex:indexPath.row];
    return cell;
}
- (CGFloat)tableView:(UITableView *)tableView heightForRowAtIndexPath:(NSIndexPath *)indexPath
{
	NSString *title = [titles objectAtIndex:indexPath.row];
	NSString *subtitle = [subtitles objectAtIndex:indexPath.row];
	
	int height = 10 + [self heightOfCellWithTitle:title andSubtitle:subtitle];
	return (height < CONST_Cell_height ? CONST_Cell_height : height);
}


- (void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath{
	
	if (self.asc == nil) {
		AnswerSelectionController *viewController = [[AnswerSelectionController alloc] initWithNibName:@"AnswerSelectionController" bundle:nil];
		self.asc = viewController;
		//currentViewController = viewController;
		// Cleanup resources
		[viewController release];
	}
	[[self navigationController] pushViewController:asc animated:YES];
	
	// Create the modal view controller

	//[tableView deselectRowAtIndexPath:indexPath animated:YES];
}



@end