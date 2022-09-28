import { View } from 'react-native'
import React, { useEffect, useState } from 'react'
import { getUserCourses, ICourse, IGetUserCourses } from '../api/courses/getUserCourses';
import { IApiResponse } from '../api/common';
import Spinner from 'react-native-loading-spinner-overlay/lib';
import { CourseListItem } from '../components/courses/CourseListItem';

interface IStartCourse {

}

export const UserCoursesPage = () => {
  const [isLoading, setIsLoading] = useState(true);
  const [courses, setCourses] = useState<ICourse[]>([]);
  const [reload, setReload] = useState(true);

  useEffect(() => {
    getUserCourses()
    .then((response: IApiResponse<IGetUserCourses>) => setCourses(response.data?.courses as ICourse[]))
    .catch((error: IApiResponse<IGetUserCourses>) => console.log(error))
    .finally(() => {
      setIsLoading(false);
    })
  }, [reload]);

  const registerForCourseButton = (props: IStartCourse) => {
    return {
      text: "Start",
      isDisabled: false,
      onPress: () => {

      }
    }
  };

  return (
    <View>
      <Spinner
        visible={isLoading}
        textContent={""}
      />
      {
        courses.map((item) => 
          <CourseListItem
            key={item.id}
            course={item}
            button={registerForCourseButton({})}
          />
        )
      }
    </View>
  )
}
