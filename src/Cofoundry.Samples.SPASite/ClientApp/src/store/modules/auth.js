import authApi from '@/api/auth';

export default {
  namespaced: true,
  state: {
    member: null
  },
  mutations: {
    setMember(state, member) {
      state.member = member;
    }
  },

  actions: {
    loadSession(context) {
      return authApi
        .getSession()
        .then(member => {
          context.commit('setMember', member);
        });
    },

    register(context, command) {
      return authApi
        .register(command)
        .then(member => {
          context.commit('setMember', member);
        })
    },

    login(context, command) {
      return authApi
        .login(command)
        .then(member => {
          context.commit('setMember', member);
        })
    },

    signOut(context) {
      return authApi
        .signOut()
        .then(member => {
          context.commit('setMember', member);
        })
    }
  }
}
