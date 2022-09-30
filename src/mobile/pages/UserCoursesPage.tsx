import { FlatList, Modal, View, StyleSheet, Pressable, Text } from 'react-native'
import React, { useEffect, useState } from 'react'
import { getUserCourses, IUserCourse, IGetUserCourses } from '../api/courses/getUserCourses';
import { IApiResponse } from '../api/common';
import Spinner from 'react-native-loading-spinner-overlay/lib';
import { CourseListItem } from '../components/courses/CourseListItem';
import { useIsFocused } from '@react-navigation/native';
import { Question } from '../components/courses/Question';
import { IFormAnswear, sendUserAnswers } from '../api/courses/sendUserAnswers';
import { CustomButton, IButtonProps } from '../components/CustomButton';
import colors from '../styles/colors';

interface IStartCourse {
  courseId: number;
  courseIndex: number;
}

export const UserCoursesPage = () => {
  const [isLoading, setIsLoading] = useState(true);
  const [courses, setCourses] = useState<IUserCourse[]>([]);
  const [currentCourseIndex, setCurrentCourseIndex] = useState(0);
  const [currentQuestionIndex, setCurrentQuestionIndex] = useState(0);
  const [startedCourse, setStartedCourse] = useState(false);
  const [formAnswers, setFormAnswers] = useState<IFormAnswear[]>([]);
  const [modalVisible, setModalVisible] = useState(false);
  const [modalContent, setModalContent] = useState("");
  const [modalCloseText, setModalCloseText] = useState("");
  const isFocused = useIsFocused();

  const nextButton: IButtonProps = {
    text: "Next",
    isDisabled: false,
    onPress: () => {
      const numberOfQuestions = courses[currentCourseIndex]?.questions.length;
      if(currentCourseIndex !== -1 && currentQuestionIndex < (numberOfQuestions - 1)) {
        setCurrentQuestionIndex(currentQuestionIndex + 1);
      }
    }
  }

  const sendAnswersButton: IButtonProps = {
    text: "Send",
    isDisabled: false,
    onPress: () => {
      console.log("Send!");
      setIsLoading(true);
      sendUserAnswers({
        courseId: courses[currentCourseIndex].id,
        formAnswers: formAnswers
      })
      .then((response) => {
        if(response.data?.passed) {
          setModalContent("Congratulations you've passed!");
          setModalCloseText("I'm the best!");
        }
        else {
          setModalContent("You didn't pass... Maybe next time!");
          setModalCloseText("I will get better!");
        }

        setModalVisible(true);
      })
      .catch((ex) => console.log(ex))
      .finally(() => setIsLoading(false))

      setCurrentCourseIndex(0);
      setCurrentQuestionIndex(0);
      setStartedCourse(false);
    }
  }

  const fetchCourses = () => {
    setIsLoading(true);
    getUserCourses()
    .then((response: IApiResponse<IGetUserCourses>) => setCourses(response.data?.courses as IUserCourse[]))
    .catch((error: IApiResponse<IGetUserCourses>) => console.log(error))
    .finally(() => setIsLoading(false));
  }

  useEffect(() => {
    fetchCourses();
  }, [isFocused]);

  const startTest = (course: IStartCourse) => {
    return {
      text: "Start",
      isDisabled: false,
      onPress: () => {
        setCurrentCourseIndex(course.courseIndex);
        setCurrentQuestionIndex(0);
        setFormAnswers(courses[course.courseIndex].questions.map((item) => {
          return {
            questionId: item.id,
            answearId: item.answers[0].id
          };
        }));
        setStartedCourse(true);
      }
    }
  };

  const modalButton: IButtonProps = {
    text: modalCloseText,
    isDisabled: false,
    onPress: () => {
      setModalVisible(false); 
      fetchCourses();
    },
    containerStyle: styles.modalButton,
    textStyle: styles.modalButtonText
  };

  if(startedCourse) {
    return (
      <Question 
      questionNumber={currentQuestionIndex}
      question={courses[currentCourseIndex].questions[currentQuestionIndex]}
      formAnswers={formAnswers}
      setFormAnswers={setFormAnswers}
      nextButton={currentQuestionIndex === (courses[currentCourseIndex].questions.length - 1) ? sendAnswersButton : nextButton}
    />)
  }

  return (
    <View style={{ backgroundColor: colors.background }}>
      <Spinner
        visible={isLoading}
        textContent={""}
      />
      <Modal
        animationType="fade"
        transparent={true}
        visible={modalVisible}
        onRequestClose={() => {
          setModalVisible(!modalVisible);
        }}
      >
        <View style={styles.centeredView}>
          <View style={styles.modalView}>
            <Text style={styles.modalText}>{modalContent}</Text>
            <CustomButton {...modalButton}/>
          </View>
        </View>
      </Modal>
      <View style={{ marginBottom: 6 }}/>
      <FlatList
        data={courses}
        renderItem={({item, index}) => (
          <CourseListItem
            course={item}
            button={startTest({ courseId: item.id, courseIndex: index })}
          />
        )}
        keyExtractor={course => course.id.toString()}
      />
      <View style={{ marginTop: 6 }}/>
    </View>
  )
}

const styles = StyleSheet.create({
  centeredView: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
    marginTop: 22
  },
  modalView: {
    margin: 20,
    backgroundColor: "white",
    borderRadius: 20,
    paddingLeft: "16%",
    paddingRight: "16%",
    paddingTop: "8%",
    paddingBottom: "8%",
    alignItems: "center",
    shadowColor: colors.shadow,
    shadowOffset: {
      width: 2,
      height: 4
    },
    shadowOpacity: 0.25,
    shadowRadius: 5,
    elevation: 6
  },
  modalButton: {
    marginVertical: 0,
    marginTop: 5
  },
  modalButtonText: {
    fontSize: 16
  },
  modalText: {
    fontSize: 20,
    marginBottom: 15,
    textAlign: "center"
  }
});
