//
//  GameViewController.h
//  BaconApp
//
//  Created by Donovan Hoffman on 14/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "ModalViewController.h"
#import "Interpreter.h"

@class AnswerSelectionController;

@interface GameViewController : ModalViewController <UITableViewDelegate, UITableViewDataSource>{
	NSMutableArray * currentQuestionFiles;
	NSMutableArray *	quizQuestions;
	NSMutableArray * data;
	NSArray * cellContent;
	UITableView * tableView;
	NSMutableArray * answersGiven;
	AnswerSelectionController * asc;
}

@property (nonatomic, retain) NSMutableArray * answersGiven;
@property (nonatomic, retain) AnswerSelectionController * asc;
@end
