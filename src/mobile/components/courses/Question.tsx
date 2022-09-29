import { View, Text, StyleSheet } from 'react-native'
import React, { useState } from 'react'
import { IQuestion } from '../../api/courses/getUserCourses'
import RadioGroup, { RadioButtonProps } from 'react-native-radio-buttons-group';
import { CustomButton, IButtonProps } from '../CustomButton';

export const Question = (question: IQuestion) => {
  const radioButtonsData = question.answers.map((answear) => {
    return {
      id: answear.id.toString(),
      label: answear.content,
      value: answear.id.toString(),
      disabled: false,
      selected: false
    }
  });

  const [radioButtons, setRadioButtons] = useState<RadioButtonProps[]>(radioButtonsData);
  const onPressRadioButton = (radioButtons: RadioButtonProps[]) => {
    setRadioButtons(radioButtons); 
  }

  const previousButton: IButtonProps = {
    text: "Previous",
    isDisabled: false,
    onPress: () => console.log("Previous")
  };

  const nextButton: IButtonProps = {
    text: "Next",
    isDisabled: false,
    onPress: () => console.log("Next")
  }

  return (
    <View style={styles.root}>
      <View style={styles.header}>
        <Text style={styles.questionContent}>{question.content}</Text>
      </View>
      <RadioGroup
          radioButtons={radioButtons}
          onPress={onPressRadioButton}
          containerStyle={styles.radioButtons}
        />
      <View style={styles.controlButtons}>
        <CustomButton
          {...previousButton}
          containerStyle={styles.button}
          textStyle={styles.button}
        />
        <View style={{width: "2%"}}/>
        <CustomButton
          {...nextButton}
          containerStyle={styles.button}
          textStyle={styles.button}
        />
      </View>
    </View>
  )
}

const styles = StyleSheet.create({
  root: {
    flex: 1,
    flexDirection: "column",
    justifyContent: "flex-start",
    alignItems: "flex-start"
  },
  questionContent: {
    fontSize: 28,
    fontWeight: "bold",
    marginTop: 15,
    marginBottom: 15,
    marginLeft: 15
  },
  header: {
    alignSelf: "flex-start"
  },
  radioButtons: {
    flex: 1,
    justifyContent: "flex-start",
    alignItems: "baseline"
  },
  button: {
    width: "45%"
  },
  buttonText: {

  },
  controlButtons: {
    flex: 1,
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "center",
    alignSelf: "center"
  }
});
