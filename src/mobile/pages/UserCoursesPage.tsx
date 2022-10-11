import { FlatList, Modal, View, StyleSheet, Text } from 'react-native'
import React, { useEffect, useState } from 'react'
import { IUserCourse, IGetUserCourses, getUserCoursesRequest } from '../api/courses/getUserCourses';
import Spinner from 'react-native-loading-spinner-overlay/lib';
import { CourseListItem } from '../components/courses/CourseListItem';
import { useIsFocused } from '@react-navigation/native';
import { Question } from '../components/courses/Question';
import { ICourseFormRequest, ICourseFormResponse, IFormAnswear, sendFormRequest } from '../api/courses/sendForm';
import { CustomButton, IButtonProps } from '../components/CustomButton';
import colors from '../styles/colors';
import { useQuery } from '../api/useQuery';

export const UserCoursesPage = () => {
  const [currentCourse, setCurrentCourse] = useState<IUserCourse>();
  const [currentQuestionIndex, setCurrentQuestionIndex] = useState(0);
  const [formAnswers, setFormAnswers] = useState<IFormAnswear[]>([]);
  const [modalVisible, setModalVisible] = useState(false);
  const [modalContent, setModalContent] = useState("");
  const [modalCloseText, setModalCloseText] = useState("");
  const isFocused = useIsFocused();

  const { 
    fetchData: getUserCourses, 
    data: coursesResponse,
    isLoading: coursesLoading,
  } = useQuery<any, IGetUserCourses>();

  const { 
    fetchData: sendForm, 
    isLoading: sendFormLoading
  } = useQuery<ICourseFormRequest, ICourseFormResponse>();

  const nextButton: IButtonProps = {
    text: "Next",
    isDisabled: false,
    onPress: () => {
      const numberOfQuestions = currentCourse?.questions.length;
      if(numberOfQuestions && currentQuestionIndex < (numberOfQuestions - 1)) {
        setCurrentQuestionIndex(currentQuestionIndex + 1);
      }
    }
  }

  const sendAnswersButton: IButtonProps = {
    text: "Send",
    isDisabled: false,
    onPress: () => {
      sendForm(sendFormRequest({
        courseId: currentCourse?.id,
        formAnswers: formAnswers
      }))
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

      setCurrentCourse(undefined);
      setCurrentQuestionIndex(0);
    }
  }

  const fetchCourses = () => {
    getUserCourses(getUserCoursesRequest());
  }

  useEffect(() => {
    fetchCourses();
  }, [isFocused]);

  const startTest = (course: IUserCourse) => {
    return {
      text: "Start",
      isDisabled: false,
      onPress: () => {
        setCurrentQuestionIndex(0);
        setCurrentCourse(course);
        setFormAnswers(course.questions.map((item) => {
          return {
            questionId: item.id,
            answearId: item.answers[0].id
          };
        }));
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

  if(coursesResponse?.courses.length === 0) {
    return (
      <View style={styles.emptyPage}>
        <Text style={styles.emptyPageText}>So empty...</Text>
        <Text style={styles.emptyPageText}>Sign up for some courses!</Text>
      </View>
    )
  }

  if(currentCourse) {
    return (
      <Question 
      questionNumber={currentQuestionIndex}
      question={currentCourse.questions[currentQuestionIndex]}
      formAnswers={formAnswers}
      onSetFormAnswers={(answers) => setFormAnswers(answers)}
      nextButton={currentQuestionIndex === (currentCourse.questions.length - 1) ? sendAnswersButton : nextButton}
    />)
  }

  return (
    <View style={{ backgroundColor: colors.background }}>
      <Spinner
        visible={coursesLoading || sendFormLoading}
        textContent={""}
      />
      <Modal
        animationType="fade"
        transparent={true}
        visible={modalVisible}
        onRequestClose={() => setModalVisible(prev => !prev)}
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
        data={coursesResponse?.courses}
        renderItem={({ item }) => (
          <CourseListItem
            course={item}
            button={startTest(item)}
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
  },
  emptyPage: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
    marginBottom: "20%"
  },
  emptyPageText: {
    fontSize: 22
  }
});
