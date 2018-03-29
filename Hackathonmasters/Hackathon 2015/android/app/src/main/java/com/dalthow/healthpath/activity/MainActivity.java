package com.dalthow.healthpath.activity;

import android.annotation.TargetApi;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Color;
import android.os.Build;
import android.os.Bundle;
import android.support.design.widget.NavigationView;
import android.support.design.widget.TabLayout;
import android.support.v4.view.ViewPager;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import com.dalthow.healthpath.R;
import com.dalthow.healthpath.adapters.FragmentPagerAdapter;
import com.dalthow.healthpath.fragments.AppointmentFragment;
import com.dalthow.healthpath.fragments.ProfileFragment;
import com.dalthow.healthpath.handler.AnimationCore;
import com.dalthow.healthpath.model.Appointment;
import com.dalthow.healthpath.model.AppointmentResult;
import com.dalthow.healthpath.model.AppointmentResultQuestions;
import com.dalthow.healthpath.model.Patient;

public class MainActivity extends AppCompatActivity {

    private final AnimationCore animationCore = new AnimationCore();
    private ImageView userPhoto;
    private TextView userDisplayName;
    private NavigationView navigationView;
    private ViewPager viewPager;
    private TabLayout tabLayout;
    private SharedPreferences pref;
    private DrawerLayout mDrawerLayout;
    public static Patient testPatient;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        toolbar.setTitleTextColor(Color.rgb(255, 255, 255));

        final ActionBar ab = getSupportActionBar();

        ab.setHomeButtonEnabled(true);

        viewPager = (ViewPager) findViewById(R.id.viewpager);
        tabLayout = (TabLayout) findViewById(R.id.tabs);

        Login();

        setupViewPager(viewPager);
        tabLayout.setupWithViewPager(viewPager);
    }

    private void setupViewPager(ViewPager viewPager) {
        testPatient = new Patient();
        testPatient.setEmail("awatertrevi@gmail.com");
        testPatient.setBirthday(new Date());
        testPatient.setHouseNumber(7);
        testPatient.setInsurance("Menzis");
        testPatient.setName("Trevi Awater");
        testPatient.setPatientId(1);
        testPatient.setRisk(385.0F);
        testPatient.setZip("6602 BS");
        testPatient.setPhone("+310638704004");
        testPatient.setPassword("nope.");

        List<Appointment> appointments = new ArrayList<Appointment>();

        SimpleDateFormat sdf = new SimpleDateFormat("dd/MM/yyyy");
        Date date1 = null, date2 = null;
        try {
            date1 = sdf.parse("21/12/2012");

            date2 = sdf.parse("21/3/2016");
        } catch (ParseException e) {
            e.printStackTrace();
        }

        Appointment testAppointment1 = new Appointment();
        testAppointment1.setSubject("Griep");
        testAppointment1.setAppointmentDate(date2);
        testAppointment1.setSymptoms("Hoge temperatuur, overgeven en hoofdpijn");
        testAppointment1.setDoctorId(1);

        Appointment testAppointment = new Appointment();
        testAppointment.setSubject("Koorts");
        testAppointment.setAppointmentDate(date1);
        testAppointment.setSymptoms("Lopende neus, rode ogen");
        testAppointment.setDoctorId(1);

        AppointmentResult testResult = new AppointmentResult();
        testResult.setPrescription("Paracetamol (20mg)");
        testResult.setNextAppointment(testAppointment1);
        AppointmentResultQuestions questionsTest = new AppointmentResultQuestions();
        questionsTest.setQuestion("How many times a day do I need to do take medication?");
        questionsTest.setAnswer("3 times a day.");

        AppointmentResultQuestions questionsTest1;
        questionsTest1 = new AppointmentResultQuestions();
        questionsTest1.setQuestion("How many times a week should I go outside?");
        questionsTest1.setAnswer("6 times a week.");
        List<AppointmentResultQuestions> questions = new ArrayList<AppointmentResultQuestions>();
        questions.add(questionsTest);
        questions.add(questionsTest1);
        testResult.setQuestions(questions);
        testResult.setDiagnose("Test diagnose");
        testResult.setPrescription("test prescription");
        testAppointment.setAppointmentResult(testResult);
        appointments.add(testAppointment1);
        appointments.add(testAppointment);
        testPatient.setAppointments(appointments);

        FragmentPagerAdapter pagerAdapter = new FragmentPagerAdapter(getSupportFragmentManager());
        pagerAdapter.addFragment(new ProfileFragment(testPatient), "Profile");
        pagerAdapter.addFragment(new AppointmentFragment(), "Appointments");
        viewPager.setAdapter(pagerAdapter);
    }

    private void Login()
    {
//        NetworkHandler.getInstance().Login();
    }

    private void restartActivity()
    {
        Intent i = getBaseContext().getPackageManager().getLaunchIntentForPackage(getBaseContext().getPackageName());
        i.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);

        startActivity(i);
    }

    @TargetApi(Build.VERSION_CODES.LOLLIPOP)
    public void revealShow(View rootView, boolean reveal, final AlertDialog dialog, int dialogViewID)
    {
        animationCore.revealShow(rootView, reveal, dialog, dialogViewID);
    }
}
