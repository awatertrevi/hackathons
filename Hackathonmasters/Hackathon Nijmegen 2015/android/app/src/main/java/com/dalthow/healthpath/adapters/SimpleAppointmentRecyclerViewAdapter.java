package com.dalthow.healthpath.adapters;

import android.content.Context;
import android.content.Intent;
import android.support.v4.app.Fragment;
import android.support.v7.widget.CardView;
import android.support.v7.widget.RecyclerView;
import android.util.TypedValue;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.util.List;

import com.dalthow.healthpath.R;
import com.dalthow.healthpath.activity.AppointmentDetailsActivity;
import com.dalthow.healthpath.model.Appointment;
import com.dalthow.healthpath.model.AppointmentResultQuestions;

public class SimpleAppointmentRecyclerViewAdapter
        extends RecyclerView.Adapter<SimpleAppointmentRecyclerViewAdapter.ViewHolder> {

    private final TypedValue mTypedValue = new TypedValue();
    private int mBackground;
    private List<Appointment> mValues;
    private Fragment mFrag;
    private Gson gson;

    public SimpleAppointmentRecyclerViewAdapter(Context context, List<Appointment> items, Fragment frag) {
        context.getTheme().resolveAttribute(R.attr.selectableItemBackground, mTypedValue, true);
        mBackground = mTypedValue.resourceId;
        mValues = items;
        mFrag = frag;
        gson = new GsonBuilder()
                .enableComplexMapKeySerialization()
                .serializeNulls()
                .create();
    }

    public String getTitleAt(int position) {
        return mValues.get(position).getSubject();
    }

    @Override
    public ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext())
                .inflate(R.layout.list_item, parent, false);
        //view.setBackgroundResource(mBackground);
        return new ViewHolder(view);
    }

    @Override
    public void onBindViewHolder(final ViewHolder holder, final int position) {
        holder.mBoundSubject = mValues.get(position).getSubject();
        holder.mBoundDoctor = Integer.toString(mValues.get(position).getDoctorId());
        holder.mBoundDate = mValues.get(position).getAppointmentDate().toString();
        holder.mBoundSymptoms = mValues.get(position).getSymptoms();

        if (mValues.get(position).getAppointmentResult() != null) {
            holder.questionses = mValues.get(position).getAppointmentResult().getQuestions();
            holder.mBoundNextAppointment = mValues.get(position).getAppointmentResult().getNextAppointment();

        }
        holder.mTextViewSubject.setText(mValues.get(position).getSubject());
        holder.mTextViewDoctor.setText(Integer.toString(mValues.get(position).getDoctorId()));
        holder.mTextViewDate.setText(mValues.get(position).getAppointmentDate().toString());

        holder.mView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Context context = v.getContext();

                //Open new Activity
                Intent intent = new Intent(context, AppointmentDetailsActivity.class);
                String serial = gson.toJson(mValues.get(position));

                intent.putExtra("test", serial);
                context.startActivity(intent);
            }
        });
    }


    @Override
    public int getItemCount() {
        return mValues.size();
    }

    public static class ViewHolder extends RecyclerView.ViewHolder {
        public final View mView;
        public final TextView mTextViewSubject;
        public final TextView mTextViewDoctor;
        public final TextView mTextViewDate;
        public String mBoundSubject;
        public String mBoundDoctor;
        public String mBoundDate;
        public String mBoundSymptoms;

        public List<AppointmentResultQuestions> questionses;
        public Appointment mBoundNextAppointment;

        CardView mBackgroudnCardView;

        public ViewHolder(View view) {
            super(view);
            mView = view;
            mTextViewSubject = (TextView) view.findViewById(R.id.appointment_subject);
            mTextViewDoctor = (TextView) view.findViewById(R.id.appointment_doctor);
            mTextViewDate = (TextView) view.findViewById(R.id.appointment_date);
            mBackgroudnCardView = (CardView) view.findViewById(R.id.background);
        }



        @Override
        public String toString() {
            return super.toString() + " '" + mTextViewSubject.getText();
        }
    }
}