import { FlatList, View } from 'react-native'
import React, { useEffect, useState } from 'react'
import { getUserCourses, ICourse, IGetUserCourses } from '../api/courses/getUserCourses';
import { IApiResponse } from '../api/common';
import Spinner from 'react-native-loading-spinner-overlay/lib';
import { CourseListItem } from '../components/courses/CourseListItem';
import { useIsFocused } from '@react-navigation/native';
import { Question } from '../components/courses/Question';
import { IFormAnswear } from '../api/courses/sendUserAnswers';
import { IButtonProps } from '../components/CustomButton';

interface IStartCourse {
  courseId: number;
  courseIndex: number;
}

export const UserCoursesPage = () => {
  const [isLoading, setIsLoading] = useState(true);
  const [courses, setCourses] = useState<ICourse[]>([]);
  const [currentCourseIndex, setCurrentCourseIndex] = useState(0);
  const [currentQuestionIndex, setCurrentQuestionIndex] = useState(0);
  const [startedCourse, setStartedCourse] = useState(false);
  const [formAnswers, setFormAnswers] = useState<IFormAnswear[]>([]);
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
      setCurrentCourseIndex(0);
      setCurrentQuestionIndex(0);
      setStartedCourse(false);
    }
  }

  useEffect(() => {
    setIsLoading(true);
    getUserCourses()
    .then((response: IApiResponse<IGetUserCourses>) => setCourses(response.data?.courses as ICourse[]))
    .catch((error: IApiResponse<IGetUserCourses>) => console.log(error))
    .finally(() => setIsLoading(false))
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
            answearId: 0
          };
        }));
        setStartedCourse(true);
      }
    }
  };

  if(startedCourse) {
    return (<Question 
      questionNumber={currentQuestionIndex}
      question={courses[currentCourseIndex].questions[currentQuestionIndex]}
      formAnswers={formAnswers}
      setFormAnswers={setFormAnswers}
      nextButton={currentQuestionIndex === (courses[currentCourseIndex].questions.length - 1) ? sendAnswersButton : nextButton}
    />)
  }

  return (
    <View>
      <Spinner
        visible={isLoading}
        textContent={""}
      />
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
