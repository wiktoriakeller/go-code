import { View, Text, StyleSheet } from 'react-native'
import React, { useEffect, useState } from 'react'
import { IQuestion } from '../../api/courses/getUserCourses'
import RadioGroup, { RadioButtonProps } from 'react-native-radio-buttons-group';
import { CustomButton, IButtonProps } from '../CustomButton';
import { IFormAnswear } from '../../api/courses/sendUserAnswers';

export interface IQuestionsData {
  questionNumber: number;
  question: IQuestion;
  formAnswers: IFormAnswear[];
  setFormAnswers: React.Dispatch<React.SetStateAction<IFormAnswear[]>>;
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
        selected: index === 0 ? true : false
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
      props.setFormAnswers(newAnswers);
    }
  }

  useEffect(() => {
    setRadioButtons(mapQuestionToRadioButtons());
  }, [props.question]);

  return (
    <View style={styles.root}>
      <Text style={styles.questionContent}>{props.question.content}</Text>
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
    fontSize: 32,
    fontWeight: "bold",
    marginTop: "35%",
    marginBottom: 25,
  },
  radioButtons: {
    alignItems: "baseline",
    marginBottom: 25
  },
  nextButton: {
    flex: 1,
    flexDirection: "row",
    alignItems: "flex-start",
    justifyContent: "center",
    alignSelf: "center",
    width: "50%"
  }
});
