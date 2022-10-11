import { View, Text, StyleSheet } from 'react-native'
import React, { useEffect, useState } from 'react'
import { IQuestion } from '../../api/courses/getUserCourses'
import RadioGroup, { RadioButtonProps } from 'react-native-radio-buttons-group';
import { CustomButton, IButtonProps } from '../CustomButton';
import { IFormAnswear } from '../../api/courses/sendForm';

export interface IQuestionsData {
  questionNumber: number;
  question: IQuestion;
  formAnswers: IFormAnswear[];
  onSetFormAnswers: (formAnswers: IFormAnswear[]) => void;
  nextButton: IButtonProps;
}

export const Question = (props: IQuestionsData) => {
  const mapQuestionToRadioButtons = () => {
    return props.question.answers.map((answear, index) => {
      return {
        id: answear.id.toString(),
        label: answear.content,
        value: answear.id.toString(),
        disabled: false,
        selected: index === 0 ? true : false,
        labelStyle: styles.labelStyle
      }
    });
  }
  
  const [radioButtons, setRadioButtons] = useState<RadioButtonProps[]>(mapQuestionToRadioButtons());
  const onPressRadioButton = (radioButtons: RadioButtonProps[]) => {
    setRadioButtons(radioButtons);
    const selectedId = radioButtons.find((item) => item.selected)?.id;
    if(selectedId) {
      const id = selectedId as unknown as number;
      const newAnswers = [...props.formAnswers];
      newAnswers[props.questionNumber].answearId = id;
      props.onSetFormAnswers(newAnswers);
    }
  }

  useEffect(() => {
    setRadioButtons(mapQuestionToRadioButtons());
  }, [props.question]);

  return (
    <View style={styles.root}>
      <View style={styles.header}>
        <Text style={styles.questionContent}>{props.question.content}</Text>
      </View>
      <RadioGroup
          radioButtons={radioButtons}
          onPress={onPressRadioButton}
          containerStyle={styles.radioButtons}
        />
      <View style={styles.nextButton}>
        <CustomButton
          {...props.nextButton}
        />
      </View>
    </View>
  )
}

const styles = StyleSheet.create({
  root: {
    flex: 1,
    flexDirection: "column",
    justifyContent: "center",
    alignItems: "center",
    alignContent: "center"
  },
  questionContent: {
    fontSize: 24,
    textAlign: "justify",
    fontWeight: "bold",
    marginTop: "8%",
    marginBottom: "5%"
  },
  header: {
    marginLeft: "6%",
    marginRight: "6%"
  },
  radioButtons: {
    alignItems: "baseline",
    alignSelf: "flex-start",
    marginBottom: "5%",
    marginLeft: "6%",
    marginRight: "6%"
  },
  labelStyle: {
    fontSize: 18
  },
  nextButton: {
    flex: 1,
    flexDirection: "row",
    alignItems: "flex-start",
    justifyContent: "center",
    alignSelf: "center",
    width: "92%"
  }
});
