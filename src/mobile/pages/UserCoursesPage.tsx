import { FlatList, View } from 'react-native'
import React, { useEffect, useState } from 'react'
import { getUserCourses, ICourse, IGetUserCourses } from '../api/courses/getUserCourses';
import { IApiResponse } from '../api/common';
import Spinner from 'react-native-loading-spinner-overlay/lib';
import { CourseListItem } from '../components/courses/CourseListItem';
import { useIsFocused } from '@react-navigation/native';
import { Question } from '../components/courses/Question';

interface IStartCourse {
  courseId: number;
  courseIndex: number;
}

export const UserCoursesPage = () => {
  const [isLoading, setIsLoading] = useState(true);
  const [courses, setCourses] = useState<ICourse[]>([]);
  const [startedTest, setStartedTest] = useState(false);
  const [currentCourse, setCurrentCourse] = useState(0);
  const isFocused = useIsFocused();

  useEffect(() => {
    setIsLoading(true);
    getUserCourses()
    .then((response: IApiResponse<IGetUserCourses>) => setCourses(response.data?.courses as ICourse[]))
    .catch((error: IApiResponse<IGetUserCourses>) => console.log(error))
    .finally(() => setIsLoading(false))
  }, [isFocused]);

  const startTest = (props: IStartCourse) => {
    return {
      text: "Start",
      isDisabled: false,
      onPress: () => {
        setStartedTest(true);
        setCurrentCourse(props.courseIndex);
      }
    }
  };

  if(startedTest) {
    return <Question {...courses[currentCourse].questions[0]} />
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
