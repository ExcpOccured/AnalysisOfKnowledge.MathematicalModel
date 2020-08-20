#include "sdk\header\testUtilities.h"

void PrintBoolValue(bool boolValue, char* message) {
    char* true_string = "true";
    char* false_string = "false";

    char* executedBoolStrValue = boolValue ? true_string : false_string;
    
    if(!(message == NULL)) {
        char* printMessage = (char*)malloc(strlen(executedBoolStrValue) + strlen(message) + 1);

        strcpy(printMessage, message);
        strcat(printMessage, executedBoolStrValue);

        printf("%s", printMessage);

        free(printMessage);
        
        return; 
    }
    
    printf("%s", executedBoolStrValue);
} 